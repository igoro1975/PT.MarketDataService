﻿using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EF.Mappings
{
    public class ScannerConfigMap : EntityTypeConfiguration<ScannerConfig>
    {
        public ScannerConfigMap()
        {
            ToTable("ScannerConfigs");

            // key
            HasKey(t => t.Id);
        }
    }
}