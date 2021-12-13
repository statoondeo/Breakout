namespace GameNameSpace
{
	public abstract class BaseTriggerGameObject : BaseGameObject
	{
		protected ICommand Command;
		protected bool Recurrent;

		protected BaseTriggerGameObject(ICommand command, bool recurrent) 
			: base() 
		{
			Command = command;
			Recurrent = recurrent;
		}
	}
}