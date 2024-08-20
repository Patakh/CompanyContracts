using System;
using System.Collections.Generic;

namespace CompanyContracts.Data;

/// <summary>
/// Договоры
/// </summary>
public partial class Contract
{
    public int Id { get; set; }

    /// <summary>
    /// ID контрагента (юридического лица)
    /// </summary>
    public int ContractorId { get; set; }

    /// <summary>
    /// ID уполномоченного лица (физического лица)
    /// </summary>
    public int AuthorizedPersonId { get; set; }

    /// <summary>
    /// Сумма договора
    /// </summary>
    public decimal ContractAmount { get; set; }

    /// <summary>
    /// Статус договора
    /// </summary>
    public string Status { get; set; } = null!;

    /// <summary>
    /// Дата подписания договора
    /// </summary>
    public DateOnly SigningDate { get; set; }

    public virtual PhysicalPerson AuthorizedPerson { get; set; } = null!;

    public virtual LegalEntity Contractor { get; set; } = null!;
}
