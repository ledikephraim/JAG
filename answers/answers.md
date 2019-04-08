#JAG Method software developer assessment
## Answers

### 1. SEO (5min)

1) add here

2) add here

3) add here

4) add here

### 2. Responsive (15m)

1) Added bootstrap row within section with ID "quote"

2) Added bootstrap row to each and every item in the main section

3) Added bootstrap grid columns for each item input within each row

4) Removed the <p> tage which enclosed all items in the section


### 3. Validation (15m)
Add any special implemetation instructions here.

### 4. JavaScript (20m)
Add any special implemetation instructions here.

### 5. Ajax calls (30m)
Add any special implemetation instructions here.

### 6. Call a REST webservice (25m)
Add any special implemetation instructions here.

Make sure that the WebHost calls the ServiceHost via REST.

### 7. ADO.Net (40m)
Add any SQL schema changes here

Column [LeadId] on [Lead] table as been updated to Identity(1,1).
Column [LeadParameterId] on [LeadParameter] table as been updated to Identity(1,1).
All changes are in the attached script named schema_updated_question7.

### 8. Poll DB (15m)
Add any SQL schema changes here

Make changes ServiceHost

### 9. SignalR (40m)
Add any SQL schema changes here

### 10. Data Analysis (30m)

1) Total Profit
**Answer**

**SQL**
SELECT SUM(Earnings)-SUM(Cost) 'Profit'
FROM [LeadDetail]


2) Total Profit (Earnings less VAT)
**Answer**

**SQL**
SELECT SUM(Earnings)-(SUM(Cost)*1.15) 'Profit (inc VAT)'
FROM [LeadDetail]

3) Profitable campaigns
**Answer**

**SQL**
SELECT 
	profits.CampaignId,
	profits.[Profit (inc VAT)]
FROM
	(SELECT CampaignId,SUM(Earnings)-(SUM(Cost)*1.15) 'Profit (inc VAT)'
		FROM [LeadDetail]
		GROUP BY CampaignId ) profits
WHERE profits.[Profit (inc VAT)] > 0

4) Average conversion rate
**Answer**

**SQL**
SELECT a.Sales,a.Accepted, CAST(a.Sales AS float)/CAST(a.Accepted AS float) ConversionRate
FROM 
(SELECT  SUM(CASE WHEN [IsSold] = 1 THEN 1 ELSE 0 END) 'Sales' , SUM(CASE WHEN [IsAccepted] =1 THEN 1 ELSE 0 END) 'Accepted'
FROM [LeadDetail]
WHERE LeadId IN (SELECT DISTINCT LeadId FROM LeadDetail) 
)a


5) Pick 2 clients based on Profit & Conversion rate & Why?
**Answer**
I would focus on clients 205, 168  because they are the most likely to complete a sale.
**SQL**
SELECT TOP 2 a.ClientId, a.Sales,a.Accepted,CAST( a.Sales AS float)/CAST(a.Accepted AS float)  ConversionRate
FROM 
(SELECT ClientId, SUM(CASE WHEN [IsSold] = 1 THEN 1 ELSE 0 END) 'Sales' , SUM(CASE WHEN [IsAccepted] =1 THEN 1 ELSE 0 END) 'Accepted'
FROM [LeadDetail]
WHERE LeadId IN (SELECT DISTINCT LeadId FROM LeadDetail) 
GROUP BY ClientId
)a
ORDER BY ConversionRate DESC
