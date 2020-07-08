using Microsoft.EntityFrameworkCore;

namespace Compuletra.RestApiExample.Data.Extensions {
    public static class DbContextExtensions {
        public static void AddGraph(this DbContext @this, object rootObject)
        {
            @this.ChangeTracker.TrackGraph(rootObject, e => {
                if (e.Entry.IsKeySet) {
                    e.Entry.State = EntityState.Unchanged;
                }
                else {
                    e.Entry.State = EntityState.Added;
                }
            });
        }
    }
}
