namespace ProgrammingClub.UnitTests.Models
{
    public abstract class BuilderBase<T>
    {

        protected T _objectToBuild;

        public T Build()
        { 
            return _objectToBuild; 
        }

        public BuilderBase<T> With(Action<T> setter)
        {
            setter(_objectToBuild);
            return this;
        }
    }

}
