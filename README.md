Hur du anänvder denna applikationen.
Du får inte ha samma användar namn som någon annan (en flaw jag inte han fixa) får då kommer dina items blandas ihop med den andra personen med samma namn
I applikationen står det vad du kan skriva för att komma till olika saker.
Du gör ett konto Med ett namn och lösenord.
Sedan loggar du in
Efteråt så har du olika val. Om du vill lägga items skriver du Add 
Den kommer då be dig skriva in vad du vill lägga till och sedan en kort descprition om det
om du vill kolla andras items så skriver du Show item market.
Då kommer du få upp allas namn och när du skriver en av deras namn kommer alla deras saker komma upp
Sedan kommer den fråga om du vill göra en request på en av dessa sakerna.
Om du skriver yes kommer du skriva raden ditt "item" du vill ha är och sedan skriva vad du vill byta den emot.
Sedan kan du kolla alla trades som har blivit accepterade genom a skriva "Aitems" vilket visar alla accepterade trades.
Om någon har skrivit en request i dina items kommer du se det när du kollar på din lista av items då kan du få 
alternativ att acceptea eller deny'a en request och då kan den andra personen se det när dem går in och kollar på dina items.
Om du sedan vill logga ut skriver du "log out". Du kan också ta bort ditt konto om du inte vill ha kvar det.



Varför jag skrev som jag skrev i koden
Finns några va jag gjorde i mitt arbete som hade kunnat vara annorlunda. 
Jag vill tästa använda filer som större del av arbetet även om vi inte hade jobbat med det så mycket. Man hade istället kunna göra listor för varje persons saker med List<string>... och så vidare. Men det stannar inte emellan program gångar.
Jag valde också att använda switch satser istället för if else för annars hade det tagit lång tid. När jag skrev hur man requestar saker så började jag försöka hitta ett sätt att hitta specifika ord i en fil men viste inte hur man gjorde det så lösningen fick bli att man väljer rad istället i filen att ställa en request till.
Jag hann inte fixa så när man skriver fel eller något så slutar inte projektet funka men det finns en del säkerhets grejer t.ex loggar du in med en nu användare skapar den en fil med ditt namn på, för annars kommer det "crasha" om du försöker gå in i din fil utan att ha några items.
