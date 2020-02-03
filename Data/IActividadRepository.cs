using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.API.Helpers;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IActividadRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<Actividad> GetActivity(int id);
        Task<IList<Actividad>> GetActividad(int idUser);
        Task<Actividad> Register(Actividad actividad);
        Task<PagedList<Actividad>> GetActividadesPaginado(UserParams userParams);
        void Update<T>(T entity) where T : class;
    }
}