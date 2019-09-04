# appium.scroll

## Syntax

```G1ANT
appium.scroll search ⟦text⟧ by ⟦text⟧ swipedir ⟦text⟧ scrollamount ⟦integer⟧
```

## Description

This command emulates scrolling in a mobile application. If no element is provided as the `search` argument value, the command scrolls the entire screen. If an element is provided, the screen is scrolled until the desired element becomes visible.

| Argument | Type | Required | Default Value | Description |
| -------- | ---- | -------- | ------------- | ----------- |
|`search`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no |  | Name of the element to be scrolled |
|`by`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no |  | Specifies an element selector: `id`, `accessibilityid`, `text`, `partialid`, `xy`, `xpath` |
|`swipedir`| [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no | up |Scrolling direction: `up` or `down` |
|`scrollamount`| [integer](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/IntegerStructure.md) | no | | Scrolling amount as a percentage of the screen: from `0` to `100` |
| `if`           | [bool](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/BooleanStructure.md) | no       | true                                                        | Executes the command only if a specified condition is true   |
| `timeout`      | [timespan](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TimeSpanStructure.md) | no       | [♥timeoutcommand](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Addon.Core/Variables/TimeoutCommandVariable.md) | Specifies time in milliseconds for G1ANT.Robot to wait for the command to be executed |
| `errorcall`    | [procedure](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ProcedureStructure.md) | no       |                                                             | Name of a procedure to call when the command throws an exception or when a given `timeout` expires |
| `errorjump`    | [label](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/LabelStructure.md) | no       |                                                             | Name of the label to jump to when the command throws an exception or when a given `timeout` expires |
| `errormessage` | [text](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/TextStructure.md) | no       |                                                             | A message that will be shown in case the command throws an exception or when a given `timeout` expires, and no `errorjump` argument is specified |
| `errorresult`  | [variable](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/VariableStructure.md) | no       |                                                             | Name of a variable that will store the returned exception. The variable will be of [error](https://manual.g1ant.com/link/G1ANT.Language/G1ANT.Language/Structures/ErrorStructure.md) structure  |

For more information about `if`, `timeout`, `errorcall`, `errorjump`, `errormessage` and `errorresult` arguments, see [Common Arguments](https://manual.g1ant.com/link/G1ANT.Manual/appendices/common-arguments.md) page.

> **Note:** the `appium.` commands require opening a mobile app with the `appium.open` command first.

## Example

This script shows scrolling a map in Google Maps. First, the map is scrolled 50 percent in the default direction (up), then, after a 5-second delay, it’s scrolled again, but this time in the opposite direction (down). After another 5-second delay, the app is closed.

> **Note:** If the first scrolling action cannot be seen, increase the first `delay` — it allows a map to be fully loaded.

```G1ANT
appium.open apppackage com.google.android.apps.maps appactivity com.google.android.maps.MapsActivity
delay 5
appium.scroll scrollamount 50
delay 5
appium.scroll swipedir down scrollamount 50
delay 5
appium.close
```
