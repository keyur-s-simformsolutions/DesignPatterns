using CQRS.Commands;
using CQRS.DataAccess;
using CQRS.Models;
using MediatR;

namespace CQRS.Handlers
{
    public class AddStudentHandler : IRequestHandler<AddStudentCommand, Student>
    {
        private readonly IDataAccess _data;
        public AddStudentHandler(IDataAccess data)
        {
            _data = data;
        }
        public Task<Student> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.AddStudent(request.firstName, request.lastName, request.age));
        }
    }
}
