using CQRS.Models;
using MediatR;

namespace CQRS.Queries
{
    public class GetStudentListQuery : IRequest<List<Student>>
    {

    }
}
