using Mediator;
using Mediator.Concrete;

class Program
{
    static void Main(string[] args)
    {
        var ConcreteMediator = new ConcreteMediator();
        var colleagueA = new ConcreteColleagueA(ConcreteMediator);
        var colleagueB = new ConcreteColleagueB(ConcreteMediator);
        ConcreteMediator.Colleague1=colleagueA;
        ConcreteMediator.Colleague2 = colleagueB;

        colleagueA.Send("Hi! Keyur");


    }
}