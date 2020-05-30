

SO Schema:
    https://meta.stackexchange.com/questions/2677/database-schema-documentation-for-the-public-data-dump-and-sede


This is the very simplest build I could do - it uses thew default Asp.net core identity from MS, the full UI is extracted in the Areas/Identity for theming.
It has 3 pages:
    QA\Index that shows the list of questions
    QA\Details shows a question and answers, and allows a new answer to be added
    QA\AddQuestion to add new Qs.

That's it, no frills at all. No search for questions, no comments, nada. Its the smallest usable site I could make.
It uses Razor pages and not MVC (but I think that's a bad choice after creating this, MVC feels more intuitive to me)

code-first migrations are enabled for debug only, so the DB should create and update itself when run, in debug.
The appsettings.json has a localdb entry for the context so it should just work on Windows.


You will need Mads Kristensen's VS extensions "Web Compiler" and "Bundler and minifier" to compile SASS into CSS and minify the js files.
Also "Microsoft Library Manager" if its not alreayd installed to handle download of js libraries too.

You will need to enter email smtp settings to send emails, but in debug all emails will be written to an "emails.txt" file too.
I've also left in the standard registration code to link to Twitter, Google and Facebook that MS provides, if you get an API key for those services.
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/?view=aspnetcore-3.1&tabs=visual-studio

For debug, config user secrets are enabled. You will probably want to put something like this in yours (right click project, "manage user secrets" to open the correct file)
The Users section is important, its used to create a default 'admin' user, though you can change the settings to what you like for your dev env.

{
	"EmailProvider": {
		"EmailServer": "auth.provider.co.uk",
		"EmailPort": 587,
		"EmailFrom": "Codidact admin",
		"EmailUser": "admin@codidact.com",
		"EmailPassword": ""
	},

	"Users": {
		"AdminName": "Admin",
		"AdminEmail": "admin@codidact.com",
		"AdminPassword": "123abc"
	}
}
