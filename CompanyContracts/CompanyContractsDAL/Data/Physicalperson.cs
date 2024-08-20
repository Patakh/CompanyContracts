using System;
using System.Collections.Generic;

namespace CompanyContracts.Data;

/// <summary>
/// Физическое лицо
/// </summary>
public partial class PhysicalPerson
{
    public int Id { get; set; }

    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public string Gender { get; set; } = null!;

    /// <summary>
    /// Возраст
    /// </summary>
    public int? Age { get; set; }

    /// <summary>
    /// Место работы
    /// </summary>
    public string? WorkPlace { get; set; }

    /// <summary>
    /// Страна проживания
    /// </summary>
    public string? Country { get; set; }

    /// <summary>
    /// Город проживания
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

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly? BirthDate { get; set; }

    public virtual ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
