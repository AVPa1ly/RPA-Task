# Description

This repository includes source code files and report examples in accordance with the task provided by Alpha Bank as a pre-test for RPA developer position.

## Task

The task was to implement an application which is capable of reading data from `data.xml` file and writing it, parsed and formatted, to `docx`, `xlsx` or `txt` format.

## Accomplished tasks

### Mandatory
- [x] Read data from file (get data without tags). Data can be read via model or regexp.
- [x] Write data to docx, xlsx or txt.
- [x] Push the results to git.

### Optional
- [x] Use WPF.
- [ ] Implement reading data from file using both regular expressions and model (user is in charge of picking parsing format).
- [x] Implement writing data in any format specified in the basic requirements.
- [x] For WPF, create buttons for each particular action.
- [ ] Create asynchronous methods.

I'm not familiar enough with async/await, this is the reason why I couldn't implement the last optional task. However, regexps aren't new for me, so if required, I can implement additional regexp parsing in a day.

## Personal Comments

The task was done in accordance with SOLID principles, implemented Strategy design pattern for parsing and reporting classes. Drawbacks: lack of expertise in WPF didn't allow to implement flexible mechanism for RadioButtons, which, I guess, would be possible provided I knew more about binding in WPF.
