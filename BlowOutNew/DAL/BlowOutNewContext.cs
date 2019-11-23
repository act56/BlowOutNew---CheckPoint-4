using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using BlowOutNew.Models;



namespace BlowOutNew.DAL
{    

    public class BlowOutNewContext : DbContext
    {

        public BlowOutNewContext() : base("BlowOutNewContext")
        {

        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
    }
}