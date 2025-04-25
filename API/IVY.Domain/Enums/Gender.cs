using System.ComponentModel;

namespace IVY.Domain.Enums;

public enum Gender
{   
    [Description("Nam")]
    Male = 1,
    [Description("Nữ")]
    Female = 2,
    [Description("Khác")]
    Other = 3

}