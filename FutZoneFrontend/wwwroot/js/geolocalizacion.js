function updateLocationFields(country, department, city, depaCode, countryCode, lat, lng, direccion, alias) {
    // Construye la cadena de ubicación completa para el campo visible del usuario
    const fullLocationDisplay = `${country}, ${department}, ${city}, ${alias}`;

    // Campo visible en el formulario (id="Ubicacion")
    // Se usa .value porque es un <input>
    document.getElementById("Ubicacion").value = fullLocationDisplay; 

    // Campos OCULTOS que se enviarán al servidor (se asume que existen, por ejemplo:
    // <input type="hidden" id="cliePais" name="cliePais" /> )
    document.getElementById("cliePais").value = country;
    document.getElementById("clieDepartamento").value = department;
    document.getElementById("clieCiudad").value = city;
    document.getElementById("clieDepaCode").value = depaCode;
    document.getElementById("cliePaisCode").value = countryCode;
    document.getElementById("clieLatitud").value = lat;
    document.getElementById("clieLongitud").value = lng;
    document.getElementById("direccion").value = direccion; // Dirección detallada de Mapbox
    document.getElementById("alias").value = alias; // Nombre corto de la calle/lugar

    // Oculta/Actualiza el mensaje de "Buscando ubicación" (ajustado para la consola, ya que no se ve el elemento en el HTML)
    // Se comenta ya que el elemento 'location-message' no existe en el HTML proporcionado.
    /*
    const messageElement = document.getElementById("location-message");
    if (messageElement) {
        messageElement.classList.remove('text-info'); 
        messageElement.classList.add('text-success');
        messageElement.textContent = 'Ubicación seleccionada y guardada.';
        setTimeout(() => {
             messageElement.style.display = 'none';
        }, 3000); 
    }
    */
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
                            // **NOTA:** La función getCountryNumericCode no está definida. 
                            // Se simula con el short_code para fines de prueba, si Mapbox lo proporciona.
                            // Si necesitas el ISO Numérico, debes implementarla.
                            // countryNumericCode = getCountryNumericCode(country);
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
            
            // Revisa si getCountryNumericCode existe, si no, usa un valor placeholder.
            // if (typeof getCountryNumericCode === 'function') {
            //     countryNumericCode = getCountryNumericCode(country);
            // }

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

    if (!document.getElementById(mapContainerId)) {
        console.error(`Contenedor del mapa con ID '${mapContainerId}' no encontrado. Se cancela la inicialización.`);
        return;
    }

    // Inicialización del mapa de Mapbox (código conservado)
    const map = new mapboxgl.Map({
        container: mapContainerId,
        style: "mapbox://styles/mapbox/streets-v12",
        center: center,
        zoom: 14,
    });

    const marker = new mapboxgl.Marker({ draggable: true })
        .setLngLat(center)
        .addTo(map);

    function onDragEnd() {
        const lngLat = marker.getLngLat();
        document.getElementById("clieLatitud").value = lngLat.lat;
        document.getElementById("clieLongitud").value = lngLat.lng;
        reverseGeocode(lngLat.lng, lngLat.lat);
        console.log("Coordenadas del Marcador (Lat, Lng):", lngLat.lat, lngLat.lng);
    }

    marker.on("dragend", onDragEnd);

    const geolocate = new mapboxgl.GeolocateControl({
        positionOptions: { enableHighAccuracy: true },
        trackUserLocation: "until_known",
        showUserHeading: true,
    });

    map.addControl(geolocate);

    const MARKER_ACCURACY_THRESHOLD = 100;

    geolocate.on("geolocate", (e) => {
        const currentAccuracy = e.coords.accuracy;
        const userLngLat = [e.coords.longitude, e.coords.latitude];

        if (currentAccuracy <= MARKER_ACCURACY_THRESHOLD) {
            marker.setLngLat(userLngLat);
            document.getElementById("clieLatitud").value = userLngLat[1];
            document.getElementById("clieLongitud").value = userLngLat[0];
            reverseGeocode(userLngLat[0], userLngLat[1]);
            console.log(`Marcador movido a ubicación precisa: ${currentAccuracy.toFixed(2)}m.`);
            
            // Detiene el seguimiento después de la primera ubicación precisa.
            setTimeout(() => {
                geolocate.trigger(); 
            }, 100);

        } else {
            console.warn(`Ubicación ignorada. Precisión (${currentAccuracy.toFixed(2)}m) es mayor al umbral de ${MARKER_ACCURACY_THRESHOLD}m.`);
        }
    });

    map.on("load", () => {
        map.resize();
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

document.addEventListener("DOMContentLoaded", () => {
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
});