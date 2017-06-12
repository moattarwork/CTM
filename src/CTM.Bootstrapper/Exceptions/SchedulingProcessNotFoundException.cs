using System;

namespace CTM.Bootstrapper.Exceptions
{
    public class SchedulingProcessNotFoundException : Exception
    {
        private const string ErrorMessage =
                "Can not start the process. Error in resolving Process dependencies from container due to invalid configuration"
            ;

        public SchedulingProcessNotFoundException() : base(ErrorMessage)
        {
        }
    }
}