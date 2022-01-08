using Ninject;
using System.Collections.Generic;
using System.Web.Mvc;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.DTOs;
using YMovies.MovieDbService.Models;
using YMovies.MovieDbService.Repositories.IRepository;
using YMovies.MovieDbService.Repositories.Repository;
using YMovies.MovieDbService.Services.IService;
using YMovies.MovieDbService.Services.Service;

namespace Ymovies.Web.Utilities
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(System.Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(System.Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            var context = new MoviesContext();

            kernel.Bind(typeof(IRepository<Movie>)).To(typeof(MovieRepository))
                                .WithConstructorArgument("context", context);

            kernel.Bind(typeof(IRepository<Genre>)).To(typeof(GenreRepository))
                                .WithConstructorArgument("context", context);

            kernel.Bind(typeof(IRepository<Cast>)).To(typeof(CastRepository))
                                .WithConstructorArgument("context", context);

            kernel.Bind(typeof(IRepository<Country>)).To(typeof(CountryRepository))
                                .WithConstructorArgument("context", context);
           
            kernel.Bind(typeof(IService<CountryDto>)).To(typeof(CountryService))
                .WithConstructorArgument("repository", new CountryRepository(context));

            kernel.Bind(typeof(IService<CastDto>)).To(typeof(CastService))
               .WithConstructorArgument("repository", new CastRepository(context));

        }
    }
}