using System.ComponentModel;

namespace ProyectoFinalIngenieria.Models.Enums
{
    public enum PaymentType
    {
        [Description("Mensual")]
        Monthly = 1,

        [Description("Quincenal")]
        BiWeekly = 2,

        [Description("Semanal")]
        Weekly = 3,

        [Description("Por Hora")]
        Hourly = 4
    }
}
