@page "/empresa/canchas"
@rendermode InteractiveServer

<div class="container-fluid py-4">
    <!-- Header con Gradiente -->
    <div class="header-gradient-empresas mb-5">
        <div class="container">
            <h1 class="display-5 fw-bold text-white mb-2">
                <i class="bi bi-football"></i> Gestión de Canchas
            </h1>
            <p class="text-white-50">Administra tus canchas, horarios y disponibilidad</p>
        </div>
    </div>

    <!-- Stats Cards -->
    <div class="row mb-4">
        <div class="col-md-3 mb-3">
            <div class="stat-card gradient-blue">
                <div class="stat-icon">
                    <i class="bi bi-pin-map-fill"></i>
                </div>
                <div class="stat-content">
                    <h5>Canchas Activas</h5>
                    <p class="stat-number">@_canchas.Count(c => c.Activa)</p>
                </div>
            </div>
        </div>
        <div class="col-md-3 mb-3">
            <div class="stat-card gradient-green">
                <div class="stat-icon">
                    <i class="bi bi-calendar-check-fill"></i>
                </div>
                <div class="stat-content">
                    <h5>Reservas Hoy</h5>
                    <p class="stat-number">@_reservasHoy</p>
                </div>
            </div>
        </div>
        <div class="col-md-3 mb-3">
            <div class="stat-card gradient-orange">
                <div class="stat-icon">
                    <i class="bi bi-clock-fill"></i>
                </div>
                <div class="stat-content">
                    <h5>Horas Libres</h5>
                    <p class="stat-number">@_horasLibres</p>
                </div>
            </div>
        </div>
        <div class="col-md-3 mb-3">
            <div class="stat-card gradient-purple">
                <div class="stat-icon">
                    <i class="bi bi-cash-coin"></i>
                </div>
                <div class="stat-content">
                    <h5>Ingresos (Mes)</h5>
                    <p class="stat-number">$@_ingresosMes</p>
                </div>
            </div>
        </div>
    </div>

    <!-- Botón Crear Nueva Cancha -->
    <div class="mb-4 d-flex justify-content-between align-items-center">
        <h4>
            <i class="bi bi-list-ul"></i> Mis Canchas
        </h4>
        <button class="btn btn-success btn-lg" @onclick="AbrirModalCrearCancha">
            <i class="bi bi-plus-circle-fill"></i> Nueva Cancha
        </button>
    </div>

    <!-- Tabla de Canchas -->
    @if (_canchas.Count == 0)
    {
        <div class="alert alert-info" role="alert">
            <i class="bi bi-info-circle-fill"></i>
            <strong>Sin canchas registradas.</strong> Crea tu primera cancha para comenzar a recibir reservas.
        </div>
    }
    else
    {
        <div class="row">
            @foreach (var cancha in _canchas)
            {
                <div class="col-lg-6 col-xl-4 mb-4">
                    <div class="cancha-card @(cancha.Activa ? "" : "deshabilitada")">
                        <!-- Header Cancha -->
                        <div class="cancha-header">
                            <div class="cancha-info">
                                <h5 class="cancha-nombre">@cancha.Nombre</h5>
                                <p class="cancha-ubicacion">
                                    <i class="bi bi-geo-alt-fill"></i> @cancha.Ubicacion
                                </p>
                            </div>
                            <div class="cancha-estado">
                                @if (cancha.Activa)
                                {
                                    <span class="badge bg-success">ACTIVA</span>
                                }
                                else
                                {
                                    <span class="badge bg-danger">DESHABILITADA</span>
                                }
                            </div>
                        </div>

                        <!-- Detalles de la Cancha -->
                        <div class="cancha-details">
                            <div class="detail-row">
                                <span class="detail-label"><i class="bi bi-rulers"></i> Dimensiones:</span>
                                <span class="detail-value">@cancha.Dimensiones</span>
                            </div>
                            <div class="detail-row">
                                <span class="detail-label"><i class="bi bi-cash"></i> Precio/Hora:</span>
                                <span class="detail-value text-success fw-bold">$@cancha.PrecioHora</span>
                            </div>
                            <div class="detail-row">
                                <span class="detail-label"><i class="bi bi-clock"></i> Horario:</span>
                                <span class="detail-value">@cancha.HoraApertura - @cancha.HoraCierre</span>
                            </div>
                            <div class="detail-row">
                                <span class="detail-label"><i class="bi bi-people-fill"></i> Capacidad:</span>
                                <span class="detail-value">@cancha.CapacidadJugadores personas</span>
                            </div>
                            <div class="detail-row">
                                <span class="detail-label"><i class="bi bi-calendar2-check"></i> Tipo Superficie:</span>
                                <span class="detail-value">@cancha.TipoSuperficie</span>
                            </div>
                        </div>

                        <!-- Horarios Disponibles -->
                        <div class="horarios-section">
                            <h6 class="horarios-title">
                                <i class="bi bi-calendar-event"></i> Horarios Disponibles
                            </h6>
                            <div class="horarios-grid">
                                @foreach (var horario in cancha.HorariosDisponibles)
                                {
                                    <div class="horario-badge @(horario.Disponible ? "disponible" : "ocupado")">
                                        <span class="horario-hora">@horario.Hora</span>
                                        <span class="horario-estado">
                                            @if (horario.Disponible)
                                            {
                                                <i class="bi bi-check-circle-fill"></i>
                                            }
                                            else
                                            {
                                                <i class="bi bi-x-circle-fill"></i>
                                            }
                                        </span>
                                    </div>
                                }
                            </div>
                        </div>

                        <!-- Días Disponibles -->
                        <div class="dias-section">
                            <h6 class="dias-title">
                                <i class="bi bi-calendar3"></i> Disponibles Estos Días
                            </h6>
                            <div class="dias-grid">
                                <span class="dia-badge @(cancha.DiasDisponibles.Contains("Lunes") ? "activo" : "inactivo")">Lun</span>
                                <span class="dia-badge @(cancha.DiasDisponibles.Contains("Martes") ? "activo" : "inactivo")">Mar</span>
                                <span class="dia-badge @(cancha.DiasDisponibles.Contains("Miércoles") ? "activo" : "inactivo")">Mié</span>
                                <span class="dia-badge @(cancha.DiasDisponibles.Contains("Jueves") ? "activo" : "inactivo")">Jue</span>
                                <span class="dia-badge @(cancha.DiasDisponibles.Contains("Viernes") ? "activo" : "inactivo")">Vie</span>
                                <span class="dia-badge @(cancha.DiasDisponibles.Contains("Sábado") ? "activo" : "inactivo")">Sáb</span>
                                <span class="dia-badge @(cancha.DiasDisponibles.Contains("Domingo") ? "activo" : "inactivo")">Dom</span>
                            </div>
                        </div>

                        <!-- Botones de Acción -->
                        <div class="cancha-actions">
                            <button class="btn btn-sm btn-primary" @onclick="() => AbrirModalEditar(cancha)">
                                <i class="bi bi-pencil-fill"></i> Editar
                            </button>
                            @if (cancha.Activa)
                            {
                                <button class="btn btn-sm btn-warning" @onclick="() => DeshabilitarCancha(cancha.Id)">
                                    <i class="bi bi-lock-fill"></i> Deshabilitar
                                </button>
                            }
                            else
                            {
                                <button class="btn btn-sm btn-info" @onclick="() => HabilitarCancha(cancha.Id)">
                                    <i class="bi bi-unlock-fill"></i> Habilitar
                                </button>
                            }
                            <button class="btn btn-sm btn-danger" @onclick="() => ConfirmarEliminar(cancha)">
                                <i class="bi bi-trash-fill"></i> Eliminar
                            </button>
                        </div>
                    </div>
                </div>
            }
        </div>
    }
</div>

<!-- Modal Crear/Editar Cancha -->
<div class="modal @(_mostrarModalCancha ? "show d-block" : "")" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header bg-gradient-primary text-white">
                <h5 class="modal-title">
                    <i class="bi bi-football"></i>
                    @(_editandoCancha != null ? "Editar Cancha" : "Crear Nueva Cancha")
                </h5>
                <button type="button" class="btn-close btn-close-white" @onclick="CerrarModalCancha"></button>
            </div>
            <div class="modal-body">
                <form>
                    <!-- Nombre de Cancha -->
                    <div class="mb-3">
                        <label for="nombre" class="form-label fw-bold">Nombre de la Cancha</label>
                        <input type="text" class="form-control" id="nombre" 
                               placeholder="Ej. Cancha Principal" 
                               @bind="_formCancha.Nombre">
                    </div>

                    <!-- Ubicación -->
                    <div class="mb-3">
                        <label for="ubicacion" class="form-label fw-bold">Ubicación</label>
                        <input type="text" class="form-control" id="ubicacion" 
                               placeholder="Ej. Calle Principal 123" 
                               @bind="_formCancha.Ubicacion">
                    </div>

                    <div class="row">
                        <!-- Dimensiones -->
                        <div class="col-md-6 mb-3">
                            <label for="dimensiones" class="form-label fw-bold">Dimensiones</label>
                            <select class="form-select" id="dimensiones" @bind="_formCancha.Dimensiones">
                                <option value="">-- Seleccionar --</option>
                                <option value="5x25">5 x 25 m</option>
                                <option value="6x36">6 x 36 m</option>
                                <option value="7x42">7 x 42 m</option>
                                <option value="8x44">8 x 44 m</option>
                                <option value="9x45">9 x 45 m</option>
                            </select>
                        </div>

                        <!-- Tipo de Superficie -->
                        <div class="col-md-6 mb-3">
                            <label for="superficie" class="form-label fw-bold">Tipo de Superficie</label>
                            <select class="form-select" id="superficie" @bind="_formCancha.TipoSuperficie">
                                <option value="">-- Seleccionar --</option>
                                <option value="Cemento">Cemento</option>
                                <option value="Pasto Sintético">Pasto Sintético</option>
                                <option value="Pasto Natural">Pasto Natural</option>
                                <option value="Madera">Madera</option>
                                <option value="Arcilla">Arcilla</option>
                            </select>
                        </div>
                    </div>

                    <div class="row">
                        <!-- Precio por Hora -->
                        <div class="col-md-6 mb-3">
                            <label for="precio" class="form-label fw-bold">Precio por Hora ($)</label>
                            <input type="number" class="form-control" id="precio" 
                                   placeholder="Ej. 50" 
                                   @bind="_formCancha.PrecioHora">
                        </div>

                        <!-- Capacidad -->
                        <div class="col-md-6 mb-3">
                            <label for="capacidad" class="form-label fw-bold">Capacidad (Jugadores)</label>
                            <input type="number" class="form-control" id="capacidad" 
                                   placeholder="Ej. 22" 
                                   @bind="_formCancha.CapacidadJugadores">
                        </div>
                    </div>

                    <div class="row">
                        <!-- Hora Apertura -->
                        <div class="col-md-6 mb-3">
                            <label for="apertura" class="form-label fw-bold">Hora de Apertura</label>
                            <input type="time" class="form-control" id="apertura" 
                                   @bind="_formCancha.HoraApertura">
                        </div>

                        <!-- Hora Cierre -->
                        <div class="col-md-6 mb-3">
                            <label for="cierre" class="form-label fw-bold">Hora de Cierre</label>
                            <input type="time" class="form-control" id="cierre" 
                                   @bind="_formCancha.HoraCierre">
                        </div>
                    </div>

                    <!-- Días Disponibles -->
                    <div class="mb-3">
                        <label class="form-label fw-bold">Días Disponibles</label>
                        <div class="dias-selector">
                            @foreach (var dia in new[] { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" })
                            {
                                <div class="form-check form-check-inline">
                                    <input class="form-check-input" type="checkbox" id="dia_@dia"
                                           @onchange="@((ChangeEventArgs e) => CambiarDia(dia, (bool)e.Value))"
                                           checked="@_formCancha.DiasDisponibles.Contains(dia)">
                                    <label class="form-check-label" for="dia_@dia">
                                        @dia.Substring(0, 3)
                                    </label>
                                </div>
                            }
                        </div>
                    </div>

                    <!-- Horarios Disponibles -->
                    <div class="mb-3">
                        <label class="form-label fw-bold">
                            <i class="bi bi-clock-fill"></i> Horarios Disponibles
                        </label>
                        <div class="horarios-selector">
                            @for (int i = 6; i <= 23; i++)
                            {
                                var hora = $"{i:D2}:00";
                                var horarioObj = _formCancha.HorariosDisponibles?.FirstOrDefault(h => h.Hora == hora);
                                var disponible = horarioObj?.Disponible ?? false;

                                <div class="form-check form-check-inline horario-check">
                                    <input class="form-check-input" type="checkbox" id="horario_@i"
                                           @onchange="@((ChangeEventArgs e) => CambiarHorario(hora, (bool)e.Value))"
                                           checked="@disponible">
                                    <label class="form-check-label" for="horario_@i">
                                        @hora
                                    </label>
                                </div>
                            }
                        </div>
                    </div>

                    <!-- Descripción -->
                    <div class="mb-3">
                        <label for="descripcion" class="form-label fw-bold">Descripción</label>
                        <textarea class="form-control" id="descripcion" rows="3"
                                  placeholder="Describe características especiales de tu cancha..."
                                  @bind="_formCancha.Descripcion"></textarea>
                    </div>

                    <!-- Activa -->
                    <div class="form-check form-switch mb-3">
                        <input class="form-check-input" type="checkbox" id="activa" 
                               @bind="_formCancha.Activa">
                        <label class="form-check-label" for="activa">
                            Cancha Activa (disponible para reservas)
                        </label>
                    </div>
                </form>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" @onclick="CerrarModalCancha">
                    <i class="bi bi-x-circle"></i> Cancelar
                </button>
                <button type="button" class="btn btn-primary" @onclick="GuardarCancha">
                    <i class="bi bi-check-circle"></i> @(_editandoCancha != null ? "Actualizar" : "Crear") Cancha
                </button>
            </div>
        </div>
    </div>
</div>

<!-- Modal Confirmar Eliminación -->
<div class="modal @(_mostrarConfirmEliminar ? "show d-block" : "")" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header bg-danger text-white">
                <h5 class="modal-title">
                    <i class="bi bi-exclamation-triangle-fill"></i> Confirmar Eliminación
                </h5>
                <button type="button" class="btn-close btn-close-white" @onclick="CancelarEliminar"></button>
            </div>
            <div class="modal-body">
                <p>¿Estás seguro de que deseas eliminar la cancha <strong>@_canchaPorEliminar?.Nombre</strong>?</p>
                <p class="text-danger">
                    <i class="bi bi-exclamation-circle-fill"></i>
                    Esta acción no se puede deshacer.
                </p>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-secondary" @onclick="CancelarEliminar">
                    <i class="bi bi-x-circle"></i> Cancelar
                </button>
                <button type="button" class="btn btn-danger" @onclick="EliminarCancha">
                    <i class="bi bi-trash-fill"></i> Sí, Eliminar
                </button>
            </div>
        </div>
    </div>
</div>

<!-- Backdrop Modal -->
@if (_mostrarModalCancha || _mostrarConfirmEliminar)
{
    <div class="modal-backdrop fade show"></div>
}

@code {
    // Clases de Datos
    public class Cancha
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Ubicacion { get; set; } = "";
        public string Dimensiones { get; set; } = "";
        public string TipoSuperficie { get; set; } = "";
        public decimal PrecioHora { get; set; }
        public int CapacidadJugadores { get; set; }
        public string HoraApertura { get; set; } = "06:00";
        public string HoraCierre { get; set; } = "23:00";
        public List<string> DiasDisponibles { get; set; } = new();
        public List<HorarioDisponible> HorariosDisponibles { get; set; } = new();
        public bool Activa { get; set; } = true;
        public string Descripcion { get; set; } = "";
    }

    public class HorarioDisponible
    {
        public string Hora { get; set; } = "";
        public bool Disponible { get; set; }
    }

    // Variables de Estado
    private List<Cancha> _canchas = new();
    private Cancha? _editandoCancha = null;
    private Cancha _formCancha = new();
    private bool _mostrarModalCancha = false;
    private bool _mostrarConfirmEliminar = false;
    private Cancha? _canchaPorEliminar = null;
    private int _reservasHoy = 3;
    private int _horasLibres = 18;
    private decimal _ingresosMes = 2500;

    protected override Task OnInitializedAsync()
    {
        CargarCanchas();
        return Task.CompletedTask;
    }

    private void CargarCanchas()
    {
        _canchas = new List<Cancha>
        {
            new Cancha
            {
                Id = 1,
                Nombre = "Cancha Principal",
                Ubicacion = "Calle Principal 123",
                Dimensiones = "8x44",
                TipoSuperficie = "Pasto Sintético",
                PrecioHora = 50,
                CapacidadJugadores = 22,
                HoraApertura = "06:00",
                HoraCierre = "23:00",
                DiasDisponibles = new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo" },
                HorariosDisponibles = GenerarHorarios(),
                Activa = true,
                Descripcion = "Cancha profesional con iluminación LED"
            },
            new Cancha
            {
                Id = 2,
                Nombre = "Cancha Secundaria",
                Ubicacion = "Avenida 5 de Mayo 456",
                Dimensiones = "6x36",
                TipoSuperficie = "Cemento",
                PrecioHora = 35,
                CapacidadJugadores = 16,
                HoraApertura = "08:00",
                HoraCierre = "22:00",
                DiasDisponibles = new List<string> { "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" },
                HorariosDisponibles = GenerarHorarios(new[] { "10:00", "14:00", "18:00" }),
                Activa = true,
                Descripcion = "Cancha deportiva estándar"
            },
            new Cancha
            {
                Id = 3,
                Nombre = "Cancha Entrenamiento",
                Ubicacion = "Parque Central",
                Dimensiones = "5x25",
                TipoSuperficie = "Pasto Natural",
                PrecioHora = 25,
                CapacidadJugadores = 12,
                HoraApertura = "07:00",
                HoraCierre = "20:00",
                DiasDisponibles = new List<string> { "Lunes", "Miércoles", "Viernes", "Domingo" },
                HorariosDisponibles = GenerarHorarios(new[] { "07:00", "17:00" }),
                Activa = false,
                Descripcion = "Cancha pequeña para entrenamientos"
            }
        };
    }

    private List<HorarioDisponible> GenerarHorarios(string[]? horariosOcupados = null)
    {
        horariosOcupados = horariosOcupados ?? Array.Empty<string>();
        var horarios = new List<HorarioDisponible>();
        
        for (int i = 6; i <= 23; i++)
        {
            var hora = $"{i:D2}:00";
            horarios.Add(new HorarioDisponible
            {
                Hora = hora,
                Disponible = !horariosOcupados.Contains(hora)
            });
        }
        
        return horarios;
    }

    private void AbrirModalCrearCancha()
    {
        _editandoCancha = null;
        _formCancha = new Cancha 
        { 
            DiasDisponibles = new List<string>(),
            HorariosDisponibles = GenerarHorarios(),
            Activa = true
        };
        _mostrarModalCancha = true;
    }

    private void AbrirModalEditar(Cancha cancha)
    {
        _editandoCancha = cancha;
        _formCancha = new Cancha
        {
            Id = cancha.Id,
            Nombre = cancha.Nombre,
            Ubicacion = cancha.Ubicacion,
            Dimensiones = cancha.Dimensiones,
            TipoSuperficie = cancha.TipoSuperficie,
            PrecioHora = cancha.PrecioHora,
            CapacidadJugadores = cancha.CapacidadJugadores,
            HoraApertura = cancha.HoraApertura,
            HoraCierre = cancha.HoraCierre,
            DiasDisponibles = new List<string>(cancha.DiasDisponibles),
            HorariosDisponibles = cancha.HorariosDisponibles.Select(h => new HorarioDisponible { Hora = h.Hora, Disponible = h.Disponible }).ToList(),
            Activa = cancha.Activa,
            Descripcion = cancha.Descripcion
        };
        _mostrarModalCancha = true;
    }

    private void CerrarModalCancha()
    {
        _mostrarModalCancha = false;
        _editandoCancha = null;
    }

    private void GuardarCancha()
    {
        if (string.IsNullOrWhiteSpace(_formCancha.Nombre))
        {
            return;
        }

        if (_editandoCancha != null)
        {
            // Actualizar cancha existente
            var index = _canchas.FindIndex(c => c.Id == _editandoCancha.Id);
            if (index >= 0)
            {
                _canchas[index] = _formCancha;
            }
        }
        else
        {
            // Crear nueva cancha
            _formCancha.Id = _canchas.Count > 0 ? _canchas.Max(c => c.Id) + 1 : 1;
            _canchas.Add(_formCancha);
        }

        CerrarModalCancha();
    }

    private void DeshabilitarCancha(int id)
    {
        var cancha = _canchas.FirstOrDefault(c => c.Id == id);
        if (cancha != null)
        {
            cancha.Activa = false;
        }
    }

    private void HabilitarCancha(int id)
    {
        var cancha = _canchas.FirstOrDefault(c => c.Id == id);
        if (cancha != null)
        {
            cancha.Activa = true;
        }
    }

    private void ConfirmarEliminar(Cancha cancha)
    {
        _canchaPorEliminar = cancha;
        _mostrarConfirmEliminar = true;
    }

    private void CancelarEliminar()
    {
        _mostrarConfirmEliminar = false;
        _canchaPorEliminar = null;
    }

    private void EliminarCancha()
    {
        if (_canchaPorEliminar != null)
        {
            _canchas.RemoveAll(c => c.Id == _canchaPorEliminar.Id);
        }
        CancelarEliminar();
    }

    private void CambiarDia(string dia, bool seleccionado)
    {
        if (seleccionado)
        {
            if (!_formCancha.DiasDisponibles.Contains(dia))
            {
                _formCancha.DiasDisponibles.Add(dia);
            }
        }
        else
        {
            _formCancha.DiasDisponibles.Remove(dia);
        }
    }

    private void CambiarHorario(string hora, bool disponible)
    {
        var horario = _formCancha.HorariosDisponibles.FirstOrDefault(h => h.Hora == hora);
        if (horario != null)
        {
            horario.Disponible = disponible;
        }
    }
}

<style>
/* Header Gradient */
.header-gradient-empresas {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    border-radius: 15px;
    padding: 40px 20px !important;
    box-shadow: 0 10px 30px rgba(102, 126, 234, 0.3);
}

/* Stat Cards */
.stat-card {
    background: white;
    border-radius: 12px;
    padding: 20px;
    display: flex;
    align-items: center;
    gap: 20px;
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.stat-card:hover {
    transform: translateY(-5px);
    box-shadow: 0 8px 25px rgba(0, 0, 0, 0.15);
}

.stat-card.gradient-blue {
    border-left: 5px solid #3b82f6;
}

.stat-card.gradient-green {
    border-left: 5px solid #10b981;
}

.stat-card.gradient-orange {
    border-left: 5px solid #f59e0b;
}

.stat-card.gradient-purple {
    border-left: 5px solid #8b5cf6;
}

.stat-icon {
    font-size: 2.5rem;
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
}

.stat-content h5 {
    font-size: 0.9rem;
    color: #6b7280;
    margin-bottom: 5px;
}

.stat-number {
    font-size: 1.8rem;
    font-weight: bold;
    color: #374151;
    margin: 0;
}

/* Cancha Card */
.cancha-card {
    background: white;
    border-radius: 12px;
    overflow: hidden;
    box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
    transition: all 0.3s ease;
    border: 2px solid transparent;
}

.cancha-card:hover {
    box-shadow: 0 8px 25px rgba(0, 0, 0, 0.15);
    border-color: #667eea;
    transform: translateY(-5px);
}

.cancha-card.deshabilitada {
    opacity: 0.7;
    background-color: #f9fafb;
}

.cancha-header {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    color: white;
    padding: 15px;
    display: flex;
    justify-content: space-between;
    align-items: flex-start;
}

.cancha-nombre {
    margin: 0;
    font-weight: bold;
    color: white;
}

.cancha-ubicacion {
    margin: 5px 0 0 0;
    color: rgba(255, 255, 255, 0.8);
    font-size: 0.9rem;
}

.cancha-details {
    padding: 15px;
    background: #f9fafb;
    border-bottom: 1px solid #e5e7eb;
}

.detail-row {
    display: flex;
    justify-content: space-between;
    padding: 8px 0;
    border-bottom: 1px solid #e5e7eb;
    font-size: 0.95rem;
}

.detail-row:last-child {
    border-bottom: none;
}

.detail-label {
    color: #6b7280;
    font-weight: 600;
}

.detail-value {
    color: #374151;
    font-weight: 500;
}

/* Horarios Section */
.horarios-section {
    padding: 15px;
    border-bottom: 1px solid #e5e7eb;
}

.horarios-title {
    margin: 0 0 12px 0;
    color: #374151;
    font-size: 0.9rem;
}

.horarios-grid {
    display: grid;
    grid-template-columns: repeat(4, 1fr);
    gap: 8px;
}

.horario-badge {
    padding: 8px;
    border-radius: 8px;
    text-align: center;
    font-size: 0.85rem;
    font-weight: 600;
    transition: all 0.2s ease;
    cursor: pointer;
}

.horario-badge.disponible {
    background: #dbeafe;
    color: #1e40af;
    border: 1px solid #3b82f6;
}

.horario-badge.disponible:hover {
    background: #bfdbfe;
    transform: scale(1.05);
}

.horario-badge.ocupado {
    background: #fee2e2;
    color: #991b1b;
    border: 1px solid #ef4444;
}

.horario-hora {
    display: block;
    margin-bottom: 4px;
}

.horario-estado {
    font-size: 0.7rem;
}

/* Días Section */
.dias-section {
    padding: 15px;
    border-bottom: 1px solid #e5e7eb;
}

.dias-title {
    margin: 0 0 12px 0;
    color: #374151;
    font-size: 0.9rem;
}

.dias-grid {
    display: grid;
    grid-template-columns: repeat(7, 1fr);
    gap: 8px;
}

.dia-badge {
    padding: 8px;
    border-radius: 8px;
    text-align: center;
    font-weight: 600;
    font-size: 0.85rem;
    transition: all 0.2s ease;
    cursor: pointer;
}

.dia-badge.activo {
    background: linear-gradient(135deg, #10b981 0%, #059669 100%);
    color: white;
}

.dia-badge.inactivo {
    background: #f3f4f6;
    color: #9ca3af;
}

/* Cancha Actions */
.cancha-actions {
    padding: 12px;
    display: flex;
    gap: 8px;
    flex-wrap: wrap;
    background: white;
    justify-content: space-between;
}

.cancha-actions .btn {
    flex: 1;
    min-width: 90px;
}

/* Modales */
.modal {
    background: rgba(0, 0, 0, 0.5);
}

.modal.show {
    background: rgba(0, 0, 0, 0.5);
}

.modal-content {
    border: none;
    border-radius: 12px;
    box-shadow: 0 20px 40px rgba(0, 0, 0, 0.2);
}

.modal-header {
    border: none;
}

.bg-gradient-primary {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%) !important;
}

.dias-selector {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(100px, 1fr));
    gap: 10px;
}

.horarios-selector {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(100px, 1fr));
    gap: 10px;
    max-height: 250px;
    overflow-y: auto;
}

.horario-check {
    padding: 8px;
    background: #f9fafb;
    border-radius: 8px;
    border: 1px solid #e5e7eb;
}

.horario-check:has(.form-check-input:checked) {
    background: #dbeafe;
    border-color: #3b82f6;
}

/* Responsive */
@media (max-width: 768px) {
    .cancha-card {
        margin-bottom: 20px;
    }

    .horarios-grid {
        grid-template-columns: repeat(3, 1fr);
    }

    .cancha-actions {
        flex-direction: column;
    }

    .cancha-actions .btn {
        width: 100%;
    }

    .stat-card {
        flex-direction: column;
        text-align: center;
    }
}
</style>
