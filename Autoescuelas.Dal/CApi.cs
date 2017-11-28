namespace Autoescuelas.Dal
{
	public static class CApi
	{
		public enum Estado
		{
		    ELABORADO,
            INICIAL,
            FINAL,
            ELIMINADO
		}

		public enum Transaccion
		{
            CREAR,
            MODIFICAR,
            ELIMINAR
		}
	}
}

