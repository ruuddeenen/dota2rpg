/* daruud id = daba2aa3-0286-4b1c-a5f9-a0a2bc10c733 */
/* hero id = ad5352b7-fdc5-4d66-952c-63df6598efba */

/* INNER JOIN */
SELECT hero.* 
FROM Heroes AS hero 
INNER JOIN LT_UserHero AS lt
ON hero.Id = lt.HeroId
WHERE lt.UserId = 'daba2aa3-0286-4b1c-a5f9-a0a2bc10c733'

/* LEFT JOIN */
SELECT users.Id, blockedUsers.Message, blockedUsers.Until, blockedUsers.ByAdminId
FROM dbo.ApplicationUsers AS users
LEFT JOIN dbo.BlockedUsers AS blockedUsers
ON users.Id = blockedUsers.UserId

/* GROUP BY*/
SELECT COUNT(Id) 'Count', MainAttribute
FROM dbo.Heroes
GROUP BY dbo.Heroes.MainAttribute