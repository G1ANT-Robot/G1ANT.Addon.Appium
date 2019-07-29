# appium.present

## Syntax

```G1ANT
appium.present search ⟦text⟧ by ⟦text⟧ result ⟦bool⟧
```

## Description

This command checks if a selected element is present on the screen.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`search`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Name of the element to be found on the screen |
|`by`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Specifies an element selector: `id`, `accessibilityid`, `text`, `partialid`, `xy`, `xpath` |
|`result`| [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no | `♥result` |Name of a variable where the command’s result will be stored: `true` if the element is present, `false` if it's not |
| `if`           | [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](https://manual.g1ant.com/link/G1ANT.Manual/appendices/common-arguments.md) page.

> **Note:** the `appium.` commands require opening a mobile app with the `appium.open` command first.

## Example

This example shows how you can handle some events that may occur while using Appium on Android Studio’s Virtual Machine. Here, when you open Gmail app in a virtual phone, you will be presented with several dialog boxes you have to react to with clicks/taps, just as if you were using this app for the first time. The `appium.present` command makes sure that a default email account is present on the screen before the robot clicks a button:

```G1ANT
appium.open apppackage com.google.android.gm appactivity com.google.android.gm.ui.MailActivityGmail
appium.click com.google.android.gm:id/welcome_tour_got_it by id
appium.present com.google.android.gm:id/owner by id
appium.click com.google.android.gm:id/action_done by id
appium.click com.google.android.gm:id/gm_dismiss_button by id
appium.click com.google.android.gm:id/gm_dismiss_button by id
delay 2
appium.scroll scrollamount 100
delay 2
appium.close
```

Let’s see what happens here exactly:

1. The robot opens Gmail app.
2. On the first welcome screen (*New in Gmail*), click *GOT IT* button to go to another screen.
3. Wait for a default email account entry to be present on the screen. When it’s there, go to the next step.
4. Click *TAKE ME TO GMAIL* button.
5. Click *Next*.
6. Click *OK*.
7. Wait 2 seconds.

7. Scroll in the default direction (up) one full screen.
8. Wait 2 seconds.
9. Close the app.

> **Note:** Button names will differ depending on a chosen Android system language, but using their internal IDs as in the example above makes the robot script immune to regional settings.