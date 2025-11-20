// Variable global para mantener la referencia al objeto .NET
let blazorReference = null;

function updateLocationFields(country, department, city, depaCode, countryCode, lat, lng, direccion, alias) {
    // Construye la cadena de ubicación completa para el campo visible del usuario
    const fullLocationDisplay = `${country}, ${department}, ${city}, ${alias}`;

    // *******************************************************************
    // CAMBIO CLAVE: Usar Blazor Interop en lugar de manipular el DOM directamente
    // Esto asegura que el modelo de C# (Empresa.Ubicacion) se actualice, 
    // permitiendo al usuario escribir después de la actualización del mapa.
    // *******************************************************************
    if (blazorReference) {
        // Llama al método [JSInvokable] en el componente Blazor
        blazorReference.invokeMethodAsync('UpdateLocationFromJs', fullLocationDisplay, lat, lng)
            .catch(error => console.error("Error al invocar Blazor UpdateLocation:", error));
    } else {
        // Fallback (solo si la referencia de Blazor no está disponible, lo cual no debería pasar)
        document.getElementById("Ubicacion").value = fullLocationDisplay;
    }


    // Los campos OCULTOS (hidden inputs) SÍ se actualizan directamente
    // ya que no están ligados a InputText de Blazor.
    document.getElementById("cliePais").value = country;
    document.getElementById("clieDepartamento").value = department;
    document.getElementById("clieCiudad").value = city;
    document.getElementById("clieDepaCode").value = depaCode;
    document.getElementById("cliePaisCode").value = countryCode;
    document.getElementById("clieLatitud").value = lat;
    document.getElementById("clieLongitud").value = lng;
    document.getElementById("direccion").value = direccion; // Dirección detallada de Mapbox
    document.getElementById("alias").value = alias; // Nombre corto de la calle/lugar

    // Oculta/Actualiza el mensaje de "Buscando ubicación" (si el elemento existe)
    const messageElement = document.getElementById("location-message");
    if (messageElement) {
        messageElement.textContent = 'Ubicación seleccionada.';
        messageElement.classList.remove('text-info');
        messageElement.classList.add('text-success');
    }
}

function reverseGeocode(lng, lat) {
    // Simplificamos la verificación del token asumiendo que ya se estableció en el HTML.
    if (typeof mapboxgl === "undefined" || !mapboxgl.accessToken) {
        console.error("Mapbox Access Token no está configurado.");
        // Se envía 'Error' para evitar fallos.
        updateLocationFields("Error", "Error", "Error", "ERROR", "ERROR", lat, lng, "Error de Token de Mapbox", "Error");
        return;
    }

    const url = `https://api.mapbox.com/geocoding/v5/mapbox.places/${lng},${lat}.json?access_token=${mapboxgl.accessToken}&language=es&types=country,region,place,locality,postcode,address,poi`;

    fetch(url)
        .then((response) => response.json())
        .then((data) => {
            let country = "País no encontrado";
            let department = "Departamento no encontrado";
            let city = "Municipio/Ciudad no encontrado";
            let departmentCode = "Código no encontrado";
            let direccion = "direccion no encontrada";
            let alias = "alias no encontrado";
            let countryNumericCode = "0"; // Valor por defecto

            if (data.features && data.features.length > 0) {
                const detailedFeature = data.features[0];
                direccion = detailedFeature.place_name_es || detailedFeature.place_name;

                // Lógica de extracción del alias/calle/punto específico (conservada)
                const firstCommaIndex = direccion.indexOf(',');
                if (firstCommaIndex !== -1) {
                    alias = direccion.substring(0, firstCommaIndex).trim();
                } else {
                    alias = detailedFeature.text_es || detailedFeature.text;
                }

                data.features.forEach((feature) => {
                    const context = feature.context || [];

                    // Busca en el contexto para obtener detalles administrativos
                    context.forEach((item) => {
                        if (item.id.startsWith("country")) {
                            country = item.text;
                            if (item.short_code) {
                                countryNumericCode = item.short_code;
                            }
                        } else if (item.id.startsWith("region")) {
                            department = item.text;
                            if (item.short_code) {
                                departmentCode = item.short_code;
                            }
                        }
                    });

                    // Intenta obtener la ciudad del feature principal o 'place'
                    const type = feature.place_type[0];
                    if ((type === 'place' || type === 'locality') && city === "Municipio/Ciudad no encontrado") {
                        city = feature.text;
                    }
                });
            }

            console.log(`Ubicación extraída: País: ${country}, Departamento: ${department} (${departmentCode}), Ciudad: ${city}, Direccion: ${direccion}, Calle: ${alias}`);

            updateLocationFields(country, department, city, departmentCode, countryNumericCode, lat, lng, direccion, alias);
        })
        .catch((error) => {
            console.error("Error en la geocodificación inversa:", error);
            updateLocationFields("Error de Red", "Error de Red", "Error de Red", "ERROR", "ERROR", lat, lng, "Error de Red", "Error");
        });
}

function initializeMap(center) {
    const mapContainerId = "mapbox-ubicacion";
    const mapElement = document.getElementById(mapContainerId);

    if (!mapElement) {
        setTimeout(() => initializeMap(center), 50);
        return;
    }

    console.log(`Contenedor '${mapContainerId}' encontrado. Inicializando Mapbox.`);

    // 2. INICIALIZACIÓN DEL MAPA
    const map = new mapboxgl.Map({
        container: mapContainerId,
        style: "mapbox://styles/mapbox/streets-v12",
        center: center,
        zoom: 14,
    });

    // 3. CREACIÓN DEL MARCADOR DRAGGABLE
    const marker = new mapboxgl.Marker({ draggable: true })
        .setLngLat(center)
        .addTo(map);

    // Función para manejar el final del arrastre (drag) del marcador
    function onDragEnd() {
        const lngLat = marker.getLngLat();
        document.getElementById("clieLatitud").value = lngLat.lat;
        document.getElementById("clieLongitud").value = lngLat.lng;
        // Llama a la geocodificación inversa para obtener la dirección
        reverseGeocode(lngLat.lng, lngLat.lat);
        console.log("Coordenadas del Marcador (Lat, Lng):", lngLat.lat, lngLat.lng);
    }

    marker.on("dragend", onDragEnd);

    // 4. CONTROL DE GEOLOCALIZACIÓN
    const geolocate = new mapboxgl.GeolocateControl({
        positionOptions: { enableHighAccuracy: true },
        trackUserLocation: "until_known",
        showUserHeading: true,
    });

    map.addControl(geolocate);

    // 5. EVENTOS VARIOS DEL MAPA

    map.on("load", () => {
        map.resize();
        // Dispara la geolocalización
        geolocate.trigger();
    });

    window.addEventListener("resize", () => {
        map.resize();
    });

    map.on("click", (e) => {
        const newLngLat = e.lngLat;
        marker.setLngLat(newLngLat);
        document.getElementById("clieLatitud").value = newLngLat.lat;
        document.getElementById("clieLongitud").value = newLngLat.lng;
        reverseGeocode(newLngLat.lng, newLngLat.lat);
        console.log("Coordenadas del Click (Lat, Lng):", newLngLat.lat, newLngLat.lng);
    });
}

/**
 * Intenta obtener la ubicación actual del usuario.
 */
function getUserLocation(successCallback, errorCallback) {
    if (navigator.geolocation) {
        const options = {
            timeout: 5000,
            maximumAge: 0,
            enableHighAccuracy: true,
        };

        const INITIAL_ACCURACY_THRESHOLD = 100;

        navigator.geolocation.getCurrentPosition(
            (position) => {
                const lng = position.coords.longitude;
                const lat = position.coords.latitude;
                const accuracy = position.coords.accuracy;

                if (accuracy > INITIAL_ACCURACY_THRESHOLD) {
                    console.warn(`Ubicación inicial ignorada. Precisión (${accuracy.toFixed(2)}m) es baja. Usando ubicación por defecto.`);
                    successCallback([lng, lat]);
                    return;
                }

                console.log(`Ubicación obtenida (Lat, Lng): ${lat}, ${lng} | Precisión (metros): ${accuracy.toFixed(2)}`);
                successCallback([lng, lat]);
            },
            (error) => {
                console.error("Error de Geolocalización:", error.message);
                errorCallback();
            },
            options
        );
    } else {
        console.warn("El navegador no soporta Geolocation.");
        errorCallback();
    }
}

/**
 * **NUEVA FUNCIÓN GLOBAL PARA INICIALIZAR DESDE BLazor**
 * Recibe la referencia del objeto .NET.
 */
window.initMapbox = function (dotNetRef) {
    // Guarda la referencia de .NET en la variable global
    blazorReference = dotNetRef;

    // Coordenadas por defecto (Ejemplo: San Salvador, [Lng, Lat])
    const defaultCenter = [-89.1715584, 13.778944];

    // Llama a getUserLocation. El callback de error llamará a initializeMap con defaultCenter.
    getUserLocation(
        (userCenter) => {
            initializeMap(userCenter);
        },
        () => {
            initializeMap(defaultCenter);
        }
    );
};