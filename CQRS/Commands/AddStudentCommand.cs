using CQRS.Models;
using MediatR;

namespace CQRS.Commands
{
   public record AddStudentCommand(string firstName, string lastName, double age) : IRequest<Student>;
    
}
