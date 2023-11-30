using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlyveLægeKBH.Commands
{
    public class CommandBase : ICommand
    {
        /*************************************************************/
        /*          Explanation of CommandBase Class                 */
        /*************************************************************/
        /*   This CommandBase class is a generic implementation of the 
        ICommand interface. It provides a flexible way to definde and
        handle commands in a ViewModel. The use of a base class like
        this is considered best practice because it promotes code
        reusability, encapsulation and separation of conserns. 
        It allows to create instances og the CommandBase for various 
        actions throughout the application without duplicating the
        command execution logic.

        Additionally it aligns with the principles of the MVVM pattern,
        where commands are often used to bind user interactions to 
        ViewModel methods.                                           */
        /*************************************************************/



        //Fields
        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;

        /*************************************************************/
        /*                   Explanation of Fields                   */
        /*************************************************************/
        /*   _executeAction: This is a delegate(Action<object>) 
        representing the action that the command will execute. It is 
        the main functionalilty the command will perform

            -canExecuteAction: This is a predicate(Predicate<object>)
        representing a condition that determines whether the command can
        be executed. A predicate is a funkction that takes an object and
        returns true or false. It is optional and can be set to null if
        no validation is needed.                                     */
        /*************************************************************/



        //Constructors        
        public CommandBase(Action<object> executeAction)

        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }

        public CommandBase(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        /*************************************************************/
        /*              Explanation of Constructors                  */
        /*************************************************************/
        /*   There are two constructors (ctor) provided. 
        
            The first ctor:
        takes only the executeAction as parameter, this is assuming 
        that the command can always be executed.
        
            the second ctor:
        takes both the executeAction and the canExecute predicate as
        parameter. This allows for a condition to determine if the 
        command is currently valid to be executed. So if the return of
        the predicate is true then the command is valid              */
        /*************************************************************/



        //Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /*************************************************************/
        /*                   Explanation of Events                   */
        /*************************************************************/
        /*   The CommandBase class implements the 'ICommand' interface
        wich requires an event named 'CanExecuteChanged'. This event is
        raised when the result of the 'CanExecute' method might have 
        changed, indicating that the UI should reevaluate wetjer the
        associated command can execute. 
        
        This is important for keeping the UI in sync with the state of
        the application. It ensures that the user can only interact with
        UI elements like buttons when it makes sense based on the current
        state of the application.

        in simpel terms is like saying: "Hey, UI, you might want to 
        check whether it's appropriate to enable or disable the button
        now because something has happened that could affect whether I
        can execute."                                                */
        /*************************************************************/



       //Method
        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }

        /*************************************************************/
        /*                  Explanation of Methods                   */
        /*************************************************************/
        /*   'CanExecute(object? paramter)':
        this method determines whether the command can be executed 
        based on the condition specified in the '_canExecuteAction' 
        predicate. If no condition is specified (predicate is null)
        it retirns true by default, indicating that the command can 
        always be executed.
            'Execute(object? paramter)':
        this method is called when the associated command is executed.
        It invokes the '_executeAction' delegate, which represent the
        actuel functionality to be performed.                        */
        /*************************************************************/





    }

}

