// Variable global para almacenar la referencia C# (necesaria para JSInterop)
var blazorDotNetReference;

// Coordenadas por defecto (Ejemplo: San Salvador, [Lng, Lat])
const defaultCenter = [-89.1715584, 13.778944]; 

// Umbral de precisión para mover el marcador (100 metros)
const MARKER_ACCURACY_THRESHOLD = 100;

/**
 * Función llamada por Blazor para inicializar Mapbox y pasar la referencia C#.
 */
window.initMapbox = (dotNetRef) => {
    // Almacena la referencia de C# globalmente
    blazorDotNetReference = dotNetRef; 

    // Inicia el proceso de geolocalización y mapa
    getUserLocation(
        (userCenter) => {
            initializeMap(userCenter);
        },
        () => {
            initializeMap(defaultCenter);
        }
    );
};

// =================================================================
// 2. FUNCIÓN DE RETORNO A BLZOR (Llama al método C#)
// =================================================================

/**
 * Llama a un método C# en Blazor para actualizar el modelo.
 * @param {string} direccion - Dirección completa (Ej. Pasaje 2 H 18, Colonia...).
 * @param {string} alias - Alias o referencia corta (Lo que se pone en el input Ubicacion).
 */
function updateBlazorLocation(direccion, alias, lat, lng) {
    const messageElement = document.getElementById("location-message");
    if (messageElement) {
        messageElement.textContent = 'Ubicación seleccionada. Edita el alias si es necesario.';
        messageElement.classList.remove('text-info'); 
        messageElement.classList.add('text-success');
    }

    if (blazorDotNetReference) {
        // Llama al método C# 'UpdateLocation' con los 4 parámetros
        blazorDotNetReference.invokeMethodAsync('UpdateLocation', direccion, alias, lat, lng) 
            .catch(error => console.error("Error al llamar a UpdateLocation en C#:", error));
    }
}

function reverseGeocode(lng, lat) {
    if (typeof mapboxgl === "undefined" || !mapboxgl.accessToken) {
        console.error("Mapbox Access Token no está configurado.");
        updateBlazorLocation("Error: Token Mapbox", "Error de Ubicación", lat, lng);
        return;
    }

    // El parámetro types= indica a Mapbox que nos devuelva varios tipos de contexto.
    const url = `https://api.mapbox.com/geocoding/v5/mapbox.places/${lng},${lat}.json?access_token=${mapboxgl.accessToken}&language=es&types=country,region,place,locality,postcode,address,poi`;

    fetch(url)
        .then((response) => response.json())
        .then((data) => {
            let country = "País no encontrado";
            let department = "Departamento no encontrado";
            let city = "Municipio/Ciudad no encontrado";
            let direccion = "Dirección completa no encontrada";
            let alias = "Alias no encontrado";
            
            if (data.features && data.features.length > 0) {
                const detailedFeature = data.features[0];
                
                // 1. EXTRAER LA DIRECCIÓN COMPLETA (place_name)
                direccion = detailedFeature.place_name_es || detailedFeature.place_name;

                // 2. EXTRAER EL ALIAS (solo la parte de la dirección antes de la primera coma)
                const firstCommaIndex = direccion.indexOf(',');
                if (firstCommaIndex !== -1) {
                    alias = direccion.substring(0, firstCommaIndex).trim();
                } else {
                    alias = detailedFeature.text_es || detailedFeature.text;
                }
                
                // 3. EXTRAER LOS NIVELES ADMINISTRATIVOS (país, región, ciudad)
                data.features.forEach((feature) => {
                    const context = feature.context || [];

                    context.forEach((item) => {
                        if (item.id.startsWith("country")) {
                            country = item.text;
                        } else if (item.id.startsWith("region")) {
                            department = item.text;
                        }
                    });
                    
                    const type = feature.place_type[0];
                    if (type === 'place' || type === 'locality') {
                        if (city === "Municipio/Ciudad no encontrado") {
                            city = feature.text;
                        }
                    }
                });
            }
            
            // 4. CONSTRUIR LA DIRECCIÓN COMPLETA SOLICITADA
            // Reemplazamos la "Direccion" de Mapbox por una más estructurada si es necesario
            // Si quieres usar todos los niveles administrativos, puedes hacer esto:
            // let fullAddress = `${alias}, ${city}, ${department}, ${country}`;
            // Pero usaremos la que Mapbox considera la mejor: 'direccion' (place_name)

            console.log(`Ubicación Detallada: ${direccion}, ${city}, ${department}, ${country}`);

            // Llamamos a Blazor con la dirección completa para el campo "Ubicacion"
            // Nota: En tu componente Blazor, establecimos que Ubicacion se llenara con el Alias. 
            // Si quieres que Ubicacion contenga la DIRECCIÓN COMPLETA, el código C# debe cambiar (ver sección 3).
            updateBlazorLocation(direccion, alias, lat, lng);
        })
        .catch((error) => {
            console.error("Error en la geocodificación inversa:", error);
            updateBlazorLocation("Error de Red / API", "Error de Ubicación", lat, lng);
        });
}

/**
 * Inicializa el mapa y los controles.
 * @param {number[]} center - Coordenadas iniciales [lng, lat].
 */
function initializeMap(center) {
    const mapContainerId = "mapbox-ubicacion";

    if (!document.getElementById(mapContainerId)) {
        console.error(`Contenedor del mapa con ID '${mapContainerId}' no encontrado.`);
        return;
    }

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
        reverseGeocode(lngLat.lng, lngLat.lat);
    }

    marker.on("dragend", onDragEnd);

    const geolocate = new mapboxgl.GeolocateControl({
        positionOptions: {
            enableHighAccuracy: true,
        },
        trackUserLocation: "until_known",
        showUserHeading: true,
    });

    map.addControl(geolocate);

    geolocate.on("geolocate", (e) => {
        const currentAccuracy = e.coords.accuracy;
        const userLngLat = [e.coords.longitude, e.coords.latitude];

        if (currentAccuracy <= MARKER_ACCURACY_THRESHOLD) {
            // Mueve el marcador y hace reverseGeocode
            marker.setLngLat(userLngLat);
            reverseGeocode(userLngLat[0], userLngLat[1]);
 
            // Detiene el tracking después de la ubicación precisa
            setTimeout(() => {
                geolocate.trigger(); 
            }, 100);

        } else {
            console.warn(
                `Ubicación ignorada. Precisión (${currentAccuracy.toFixed(2)}m) es mayor al umbral de ${MARKER_ACCURACY_THRESHOLD}m.`
            );
        }
    });

    map.on("load", () => {
        map.resize();
        geolocate.trigger();
        // Llama a reverseGeocode inicialmente con la ubicación del centro (puede ser por defecto o geolocalizada)
        const initialLngLat = map.getCenter();
        reverseGeocode(initialLngLat.lng, initialLngLat.lat);
    });

    window.addEventListener("resize", () => {
        map.resize();
    });

    map.on("click", (e) => {
        const newLngLat = e.lngLat;
        marker.setLngLat(newLngLat);
        reverseGeocode(newLngLat.lng, newLngLat.lat);
    });
}

/**
 * Intenta obtener la ubicación actual del usuario.
 * @param {function(number[])} successCallback - Función a ejecutar con las coordenadas [lng, lat].
 * @param {function()} errorCallback - Función a ejecutar si falla.
 */
function getUserLocation(successCallback, errorCallback) {
    if (navigator.geolocation) {
        const options = {
            timeout: 5000, 
            maximumAge: 0, 
            enableHighAccuracy: true,
        };

        const INITIAL_ACCURACY_THRESHOLD = 1000; // Umbral más alto para la ubicación inicial

        navigator.geolocation.getCurrentPosition(
            (position) => {
                const lng = position.coords.longitude;
                const lat = position.coords.latitude;
                const accuracy = position.coords.accuracy;

                if (accuracy > INITIAL_ACCURACY_THRESHOLD) {
                    console.warn(`Ubicación inicial ignorada. Precisión (${accuracy.toFixed(2)}m) es baja.`);
                    // En este caso, retornamos la ubicación de todas formas, pero con el warning.
                }

                successCallback([lng, lat]);
            },
            (error) => {
                console.error("Error de Geococalización:", error.message);
                errorCallback(); // Usa la ubicación por defecto
            },
            options
        );
    } else {
        console.warn("El navegador no soporta Geolocation.");
        errorCallback(); // Usa la ubicación por defecto
    }
}
