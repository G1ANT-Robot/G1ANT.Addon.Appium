# appium.gettext

## Syntax

```G1ANT
appium.gettext search ⟦text⟧ by ⟦text⟧ result ⟦text⟧
```

## Description

This command gets text from an element in a chosen mobile application.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`search`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Name of the element to get text from |
|`by`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | yes |  | Specifies an element selector: `id`, `accessibilityid`, `text`, `partialid`, `xpath` |
|`result`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | `♥result` | Name of a variable where the text of a specified element will be stored |
| `if`           | [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](https://manual.g1ant.com/link/G1ANT.Manual/appendices/common-arguments.md) page.

> **Note:** the `appium.` commands require opening a mobile app with the `appium.open` command first.

## Example

This example shows how you can get some text displayed in a mobile application. The script opens Google  Drive application, clicks its menu button found by its XPath and then gets the text contained in the last element of the menu, which shows the available Google Drive storage space for the current Google account (this element is found by its ID). The extracted text is stored in the default `♥result` variable and is displayed with the `dialog` command:

```G1ANT
appium.open apppackage com.google.android.apps.docs appactivity com.google.android.apps.docs.drive.startup.StartupActivity
appium.click //android.widget.ImageButton[@index='0'] by xpath
appium.gettext com.google.android.apps.docs:id/storage_summary by Id
dialog ♥result
appium.close
```

> **Note:** The default XPath to the Google Drive app menu element contains `@content-desc` attribute with a textual description. Since this description varies depending on the app/phone language, it’s smarter to use an attribute that is language neutral — `@index`, for example.