using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Entidades.Entidades;
using VideoClub.WebMVC.Models.Calificacion;
using VideoClub.WebMVC.Models.Empleado;
using VideoClub.WebMVC.Models.Estado;
using VideoClub.WebMVC.Models.Genero;
using VideoClub.WebMVC.Models.Localidad;
using VideoClub.WebMVC.Models.Pelicula;
using VideoClub.WebMVC.Models.Provincias;
using VideoClub.WebMVC.Models.Socio;
using VideoClub.WebMVC.Models.Soporte;

namespace VideoClub.WebMVC.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            LoadCalificacionMapping();
            LoadPeliculasMapping();
            LoadGeneroMapping();
            LoadSoporteMapping();
            LoadProvinciaMapping();
            LoadEstadoMapping();
            LoadLocalidadesMapping();
            LoadSocioMapping();
            LoadEmpleadoMapping();
        }

        private void LoadEmpleadoMapping()
        {
            CreateMap<Empleado, EmpleadoListVm>()
                .ForMember(dest => dest.TipoDeDocumento,
                    opt => opt.MapFrom(src => src.TipoDeDocumento));
            CreateMap<Empleado, EmpleadoListVm>()
                .ForMember(dest => dest.Localidad,
                    opt => opt.MapFrom(src => src.Localidad));
            CreateMap<Empleado, EmpleadoListVm>()
                .ForMember(dest => dest.Provincia,
                    opt => opt.MapFrom(src => src.Provincia));
        }

        private void LoadSocioMapping()
        {
            CreateMap<Socio, SocioListVm>()
                .ForMember(dest => dest.TipoDeDocumento,
                    opt => opt.MapFrom(src => src.TipoDeDocumento));
            CreateMap<Socio, SocioListVm>()
                .ForMember(dest => dest.Localidad,
                    opt => opt.MapFrom(src => src.Localidad));
            CreateMap<Socio, SocioListVm>()
                .ForMember(dest => dest.Provincia,
                    opt => opt.MapFrom(src => src.Provincia));
            CreateMap<Socio, SocioListVm>()
                .ForMember(dest => dest.FechaDeNacimiento, 
                    opt => opt.MapFrom(src => src.FechaDeNacimiento.ToShortDateString()));
        }

        private void LoadPeliculasMapping()
        {
            CreateMap<Pelicula, PeliculaListVm>()
                .ForMember(dest => dest.FechaIncorporacion,
                    opt => opt.MapFrom(src => src.FechaIncorporacion.ToShortDateString()));
            CreateMap<Pelicula, PeliculaListVm>()
                .ForMember(dest => dest.Calificacion,
                    opt => opt.MapFrom(src => src.Calificacion));
            CreateMap<Pelicula, PeliculaListVm>()
                .ForMember(dest => dest.Genero,
                    opt => opt.MapFrom(src => src.Genero));
            CreateMap<Pelicula, PeliculaListVm>()
                .ForMember(dest => dest.Soporte,
                    opt => opt.MapFrom(src => src.Soporte));
            CreateMap<Pelicula, PeliculaListVm>()
                .ForMember(dest => dest.Estado,
                    opt => opt.MapFrom(src => src.Estado));

        }

        private void LoadCalificacionMapping()
        {
            CreateMap<Calificacion, CalificacionEditVm>().ReverseMap();
        }

        private void LoadGeneroMapping()
        {
            CreateMap<Genero, GeneroEditVm>().ReverseMap();

        }

        private void LoadSoporteMapping()
        {
            CreateMap<Soporte, SoporteEditVm>().ReverseMap();
        }

        private void LoadProvinciaMapping()
        {
            CreateMap<Provincia, ProvinciaEditVm>().ReverseMap();
        }
        private void LoadEstadoMapping()
        {
            CreateMap<Estado, EstadoEditVm>().ReverseMap();
        }
        private void LoadLocalidadesMapping()
        {

            CreateMap<Localidad, LocalidadListVm>()
                .ForMember(dest => dest.Provincia, opt => opt.MapFrom(src => src.Provincia.NombreProvincia));
            CreateMap<Localidad, LocalidadEditVm>().ReverseMap();
         
        }

    }
}