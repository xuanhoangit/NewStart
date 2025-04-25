
using System;
using System.ComponentModel;
using System.Reflection;

namespace IVY.Domain.Libs;
public static class EnumHelper
{
    /// <summary>
    /// Lấy chuỗi mô tả từ enum, nếu có [Description], ngược lại trả về tên enum.
    /// </summary>
    /// <param name="value">Giá trị enum</param>
    /// <returns>Mô tả chuỗi</returns>
    public static string GetEnumDescription(Enum value)
    {
        if (value == null) return string.Empty;

        FieldInfo field = value.GetType().GetField(value.ToString());

        if (field == null) return value.ToString();

        DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

        return attribute?.Description ?? value.ToString();
    }

}