using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
public abstract class Actor : IActor
{
    protected IEnumerable<dynamic> Abilities;
    public Actor()
    {
        Abilities = [];
    }
    public virtual IActor Answers<T,R>(IQuestion<T,R> question, out R response)
    {
        response = Answers(question);
        return this;
    }
    public virtual R Answers<T, R>(IQuestion<T,R> question)
    {
        question.EnabledBy(Abilities.Select(a => a.GetEnabler()).First(e => e is T)).AnsweredTo(this, out R response);
        return response;
    }
    public virtual IActor HasAbility<T>(IAbility<T> ability)
    {
        Abilities = [.. Abilities, ability];
        return this;
    }
    public virtual IActor Performs(IBehaviour task)
    {
        task.PerformedAs(this);
        return this;
    }
    public virtual IActor Performs<T>(IInteraction<T> interaction)
    {
        interaction.EnabledBy(Abilities.Select(a => a.GetEnabler()).First(e => e is T)).PerformedBy(this);
        return this;
    }
}

