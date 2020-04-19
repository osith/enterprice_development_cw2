using System;

namespace CW_2
{
    public class BaseEntityContextService : IDisposable
    {
        protected DBContextContainer DbContext { get; set; }

        public BaseEntityContextService()
        {
            DbContext = new DBContextContainer();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
        }
    }
}
