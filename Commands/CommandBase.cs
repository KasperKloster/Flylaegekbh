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
        //**************************************************************************//
        /// <summary>
        /// Explanation of CommandBase Class:
        ///   
        /// - The CommandBase class serves as a generic implementation of the ICommand interface.
        /// 
        /// - It provides a flexible approach for defining and handling commands in a ViewModel.
        /// 
        /// - The use of a base class for commands adheres to best practices by promoting code reusability,
        ///   encapsulation, and separation of concerns. This allows the creation of instances of CommandBase
        ///   for various actions across the application without duplicating command execution logic.
        /// 
        /// - Aligns with the principles of the MVVM (Model-View-ViewModel) pattern, where commands are commonly
        ///   utilized to bind user interactions to ViewModel methods.
        /// </summary>
        //**************************************************************************//





        //----------------------------------------Fields--------------------------------------//
        //**************************************************************************//
        /// <summary>
        /// Explanation of Fields:
        ///   
        /// - _executeAction: This is a delegate (Action<object>) representing the action that the command will execute.
        ///   It encapsulates the main functionality that the command will perform.
        /// 
        /// - _canExecuteAction: This is a predicate (Predicate<object>) representing a condition that determines whether
        ///   the command can be executed. A predicate is a function that takes an object and returns true or false. 
        ///   It is optional and can be set to null if no validation is needed.
        /// </summary>
        //**************************************************************************//

        private readonly Action<object> _executeAction;
        private readonly Predicate<object> _canExecuteAction;
        private Action updateAirCrew;

        //----------------------------------------Constructors--------------------------------------//
        //**************************************************************************//
        /// <summary>
        /// Explanation of Constructors:
        ///   
        /// There are two constructors provided.
        /// 
        /// - The first constructor (ctor) takes only the executeAction as a parameter, assuming that the command can always be executed.
        /// 
        /// - The second constructor (ctor) takes both the executeAction and the canExecute predicate as parameters. 
        ///   This allows for a condition to determine if the command is currently valid to be executed. 
        ///   If the return of the predicate is true, then the command is considered valid.
        /// </summary>
        //**************************************************************************//

        public CommandBase(Action<object> executeAction)

        {
            _executeAction = executeAction;
            _canExecuteAction = null;
        }

        public CommandBase(Action updateAirCrew)
        {
            this.updateAirCrew = updateAirCrew;
        }

        public CommandBase(Action<object> executeAction, Predicate<object> canExecuteAction)
        {
            _executeAction = executeAction;
            _canExecuteAction = canExecuteAction;
        }

        //----------------------------------------Events--------------------------------------//
        //**************************************************************************//
        /// <summary>
        /// Explanation of Events:
        ///   
        /// The CommandBase class implements the 'ICommand' interface, which requires an event named 'CanExecuteChanged'. 
        /// This event is raised when the result of the 'CanExecute' method might have changed, indicating that the UI should reevaluate 
        /// whether the associated command can execute.
        /// 
        /// This is crucial for keeping the UI in sync with the state of the application. It ensures that the user can only interact 
        /// with UI elements like buttons when it makes sense based on the current state of the application.
        /// 
        /// In simple terms, it's like saying: "Hey, UI, you might want to check whether it's appropriate to enable or disable the button
        /// now because something has happened that could affect whether I can execute."
        /// </summary>
        //**************************************************************************//

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        //----------------------------------------Method--------------------------------------//
        //**************************************************************************//
        /// <summary>
        /// Explanation of Methods:
        ///   
        /// - 'CanExecute(object? parameter)': This method determines whether the command can be executed based on the condition 
        ///   specified in the '_canExecuteAction' predicate. If no condition is specified (predicate is null), it returns true by default,
        ///   indicating that the command can always be executed.
        /// 
        /// - 'Execute(object? parameter)': This method is called when the associated command is executed. It invokes the '_executeAction'
        ///   delegate, which represents the actual functionality to be performed.
        /// </summary>
        //**************************************************************************//

        public bool CanExecute(object parameter)
        {
            return _canExecuteAction == null ? true : _canExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            _executeAction(parameter);
        }
    }

}

