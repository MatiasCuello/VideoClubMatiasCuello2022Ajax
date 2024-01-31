using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Entidades.Entidades;

namespace VideoClub.Repositorios.Repositorios.Facades
{
    public interface IRepositorioTiposDeDocumentos
    {
        void Guardar(TipoDeDocumento tipoDeDocumento);
        List<TipoDeDocumento> GetLista();
        void Borrar(int tipoDeDocumentoId);
        TipoDeDocumento GetTipoPorId(int id);
        bool Existe(TipoDeDocumento tipoDeDocumento);
        bool EstaRelacionado(TipoDeDocumento tipoDeDocumento);
    }
}
