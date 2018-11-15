<!-- default file list -->
*Files to look at*:

* **[CustomSchedulerCommandFactoryService.cs](./CS/WindowsFormsApplication1/CustomSchedulerCommandFactoryService.cs) (VB: [CustomSchedulerCommandFactoryService.vb](./VB/WindowsFormsApplication1/CustomSchedulerCommandFactoryService.vb))**
* [Form1.cs](./CS/WindowsFormsApplication1/Form1.cs) (VB: [Form1.vb](./VB/WindowsFormsApplication1/Form1.vb))
<!-- default file list end -->
# How to replace a default SchedulerControl's command with a custom one


<p>This example illustrates the technique used to modify the functionality of existing SchedulerControl commands.<br>The SchedulerControl exposes the <strong>ISchedulerCommandFactoryService</strong> interface that enables you to substitute the default command with your own custom command. <br><br>First, create a custom command class inherited from the command that you wish to replace. Override the required methods of the command. Then, create a class that implements the <strong>ISchedulerCommandFactoryService </strong>interface. In this class, you need to implement the <strong>CreateCommand</strong> method to create an instance of a custom command class. An identifier of the currently processed command is passed as a parameter of this method.<br><br>Finally, use the custom class that implements the <strong>ISchedulerCommandFactoryService</strong> interface to substitute the default command service. In this case, instead of the default command, the command you implemented will be used by the SchedulerControl.<br><br>For demonstration purposes, the default <strong>SplitAppointmentOperationCommand</strong> command is replaced with a custom command in this example. The default <strong>SplitAppointmentOperationCommand</strong> command is extended with the SplitAppointmentCommandStep property to define a fixed split operation's step.</p>

<br/>


