# WolfHowl
A workflow platform that could be quickly developed for workflow applications and easy to configure.

# Workflow Activities
 * InitializeActivity
 * PerformActivity
 * HastenActivity
 * ForwardActivity
 * EndActivity
 * EmailActivity
 * SmsActivity

Developers could create their **WORKFLOW LIBRARY***(DEFINITION)* with these activities in **Visual Studio**.

# Conventions
 * `FlowChart` must be the root container of all activities;
 * Start with `InitializeActivity` and end with `EndActivity`;
 * Activity has a property `state` which is stand for the status of current activity. The state must be unique and it could be named as `010`, `020`, `100`, `110`, ...
 * 8 parameters should be defined in your **WORKFLOW LIBRARY***(DEFINITION)*: `instanceName`, `createUserID`, `createUserName`, `nextActivityID`, `nextUserID`, `nextUserName`, `comment`, `basicBusinessInfo`