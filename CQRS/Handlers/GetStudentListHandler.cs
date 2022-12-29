using CQRS.DataAccess;
using CQRS.Models;
using CQRS.Queries;
using MediatR;

namespace CQRS.Handlers
{
    public class GetStudentListHandler : IRequestHandler<GetStudentListQuery, List<Student>>
    {
        private readonly IDataAccess _data;
        public GetStudentListHandler(IDataAccess data)
        {
            _data = data;
        }
        public Task<List<Student>> Handle(GetStudentListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.GetStudents());
        }
    }
}
