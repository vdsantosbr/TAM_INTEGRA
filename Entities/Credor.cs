

namespace Entities
{
    public class Credor
    {
        public string SP_CODIGO_CREDOR_DESPESA { get; set; }
        public string CREDOR { get; set; }
        public Credor(string SP_CODIGO_CREDOR_DESPESA, string CREDOR)
        {
            this.SP_CODIGO_CREDOR_DESPESA = SP_CODIGO_CREDOR_DESPESA;
            this.CREDOR = CREDOR;
        }
    }
}
