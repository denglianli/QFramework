﻿

namespace QFramework {

    public interface INoneOfMatcher<TEntity> : ICompoundMatcher<TEntity> where TEntity : class, IEntity {
    }
}
