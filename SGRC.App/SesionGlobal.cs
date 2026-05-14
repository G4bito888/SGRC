using System;

namespace SGRC.App
{
    public static class SesionGlobal
    {
        public static string RolActual 
        { 
            get; 
            set; 
        } = "Admin"; 
        public static DateTime FechaSistema 
        { 
            get; 
            set; 
        } = DateTime.Now;
    }
}