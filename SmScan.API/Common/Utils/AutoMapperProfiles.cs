using AutoMapper;
using SmScan.API.Domains.Categorias;
using SmScan.API.Domains.IngestaReferencia;
using SmScan.API.Domains.PaisesOrigen;
using SmScan.API.Domains.Productos;
using SmScan.API.Domains.Usuarios;
using SmScan.DTO.Categorias;
using SmScan.DTO.IngestaReferencia;
using SmScan.DTO.PaisesOrigen;
using SmScan.DTO.Productos;
using SmScan.DTO.Usuarios;

namespace SmScan.API.Common.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            #region Categorias
            CreateMap<Categoria, CategoriaResponseDto>();
            CreateMap<CategoriaRequestDto, Categoria>();
            #endregion

            #region IngestaRef
            CreateMap<IngestaReferencia, IngestaRefResponseDto>();
            CreateMap<IngestaRefRequestDto, IngestaReferencia>();
            #endregion

            #region PaisesOrigen
            CreateMap<PaisOrigen, PaisOrigenResponseDto>();
            CreateMap<PaisOrigenRequestDto, PaisOrigen>();
            #endregion

            #region Productos
            CreateMap<Producto, ProductoResponseDto>();
            CreateMap<ProductoRequestDto, Producto>();
            #endregion

            #region Usuarios
            CreateMap<Usuario, UsuarioResponseDto>();
            CreateMap<UsuarioRequestDto, Usuario>();
            #endregion
        }
    }
}
