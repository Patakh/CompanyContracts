using System;
using System.Collections.Generic;

namespace CompanyContracts.Data;

/// <summary>
/// Юридическое лицо
/// </summary>
public partial class LegalEntity
{
    public int Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// ИНН
    /// </summary>
    public string Inn { get; set; } = null!;

    /// <summary>
    /// ОГРН
    /// </summary>
    public string Ogrn { get; set; } = null!;

    /// <summary>
    /// Страна регистрации
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Город регистрации
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Электронная почта
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? Phone { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
