# Contributing to Codidact
Contributions are very welcome! Codidact is an open, community-run project, and that means the code too.

## What needs doing?
 - **Bugs** are reported here on GitHub. Have a look at the open issues tagged `type: bug` to find something that needs fixing.
 - **Requests for features** are first discussed on our [forum](https://forum.codidact.org/) to create consensus around their
   specification. Once consensus is built, an issue will be created here containing the details of the feature to be built. These
   issues are tagged `type: feature request`.
   
Once you've picked what you're going to work on, please **leave a comment** on the issue to indicate you're planning to work on it;
this helps us reduce wasted effort. If you need time to work on an issue, that's absolutely fine, but please **keep us updated**
with comments on the issue - if we don't hear from you for a few weeks, we may assume you've given up working on that issue and
give it to someone else.

## What's the workflow?
If you're an **external contributor**, please fork the repository and make your changes, then send a pull request targeting our
`development` branch.

If you're an **internal contributor**, please create a topic branch and make your changes there, then create a pull request targeting
`development`.

In either case, **status checks are required to pass** and **at least one approving review** is required from the team before any
pull request can be merged. If status checks don't pass, we won't be able to merge - there are no exceptions, so please fix the
failures and commit again. You can always mark your pull request as [WIP] (for "Work In Progress") while you're still trying to make
it work.

## What standards are there?
We have code style and standards documents for each applicable language. Please make sure you follow these if possible; if there's a
good reason why not, please document it in your code, add a linter exception, and let us know why in your pull request. Here they are:

 * [Style guide for HTML](https://github.com/codidact/core/wiki/Code-standards:-HTML)

We also have some [guidelines for commit messages](https://github.com/codidact/core/wiki/Committing-guidelines). Again, please follow 
these where possible, as they help us to keep a cohesive commit history and see how the project has developed.
