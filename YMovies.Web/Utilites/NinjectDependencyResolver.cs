using Ninject;
using System.Collections.Generic;
using System.Web.Mvc;
using YMovies.MovieDbService.DatabaseContext;
using YMovies.MovieDbService.DTOs;
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

            kernel.Bind(typeof(IService<TypeDto>)).To(typeof(TypeService))
                .WithConstructorArgument("repository", new TypeRepository(context));

            kernel.Bind(typeof(IService<CountryDto>)).To(typeof(CountryService))
                .WithConstructorArgument("repository", new CountryRepository(context));

            kernel.Bind(typeof(IService<CastDto>)).To(typeof(CastService))
               .WithConstructorArgument("repository", new CastRepository(context));

            kernel.Bind(typeof(IService<GenreDto>)).To(typeof(GenreService))
             .WithConstructorArgument("repository", new GenreRepository(context));      

            kernel.Bind(typeof(IService<MediaDto>)).To(typeof(MovieService)).InSingletonScope()
               .WithConstructorArgument("repository", new MovieRepository(context));

            kernel.Bind(typeof(ISearchService)).To(typeof(SearchService))
                .WithConstructorArgument("repository", new MovieRepository(context));

            kernel.Bind(typeof(LikesService)).To(typeof(LikesService))
                .WithConstructorArgument("context", context);

            kernel.Bind(typeof(WatchService)).To(typeof(WatchService))
                .WithConstructorArgument("context", context);
        }
    }
}