I ended up building something a lot simpler than what I originally set out. 

Imagine a use case where we have data about 50,000 concert lineup interests that needs to be uploaded to our database. This cannot be done in a single REST API since this could take a long time to respond. I used a steeltoe rabbitMQ connector that writes (CountryThunderWriter) these interests to a queue one at a time. Then, there is a monitor (CountryThunderMonitor) that reads from the queue and calls the API to insert interest data (CountryThunder.Lineup.RSVP.API).

For the backing service I am using rabbitMQ and Azure SQL database
