﻿namespace SistemadeVentas.Api.Utilidad
{
    public class Response<T>
    {
        public bool Status { get; set; }

        public T value { get; set; }

        public string msg { get; set; }
    }
}
