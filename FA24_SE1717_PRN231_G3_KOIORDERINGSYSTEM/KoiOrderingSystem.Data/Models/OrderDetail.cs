﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace KoiOrderingSystem.Data.Models;

public partial class OrderDetail
{
    public int Id { get; set; }

    public int? KoiFishId { get; set; }

    public int? KoiOrderId { get; set; }

    public int? ConsultingId { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? CreateDate { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public DateTime? UpdateDate { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual KoiFish KoiFish { get; set; }

    public virtual KoiOrder KoiOrder { get; set; }
}