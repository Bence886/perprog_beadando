# Párhuzamos és elosztott rendszerek programozása

Beadandó

# Fényforrás irányának keresése sugárkövetéssel

**Feladat meghatározás** :

A szakdolgozatom a fák növekedésének szimulációjáról szól, amiben jelentős szerepet játszik a fény és legfőképpen annak iránya az ágvéghez képest. Ennek meghatározása a feladat.

**Sugárkövetés** :

A sugárkövetés egy képalkotási technológia, ami a fény útjának visszakövetésével határozza meg a pixelek színét egy téglalapon így alkotva meg a képet. Ezt a tulajdonságot kihasználva a technika használható arra is hogy a tér adott pontján meghatározzuk milyen irányból mennyire erős fény érkezik. Erre szükségem lesz a szakdolgozatomban a fák növekedési irányának meghatározása részeként.

**Monte Carlo módszer:**

A Monte Carlo módszer magába foglal egy nagy csoport algoritmus fajtát ami mind ismételt véletlenszerű mintavételezésen alapszik. A véletlenszerű mintavételezés megoldja azokat a problémákat amik előkerülhetnek egy determinisztikus megoldás alkalmazásakor.

A sugárkövetés is pont egy ilyen probléma mivel ha nem véletlenszerűen hanem egy rácson futtatnánk a sugárkövetése akkor nagyon pici eséllyel találná el csak a fényforrást, főleg ha az pontszerűen van modellezve. A sugarak random pattanása megfelelően szimulálja a valóságban mikroszkopikus egyenetlenségekkel rendelkező felületeken szóródó fényt is.

**Program** :

Az én esetemben a végeredmény nem kép lesz hanem csak a fény iránya egy pontban. Továbbá az egyszerűség kedvéért nem kezelek csak egyféle diffuse felületet típust.

A program bemenete egy XML fájl ami tartalmazza azokat a pontokat amikben végezni szeretném a vizsgálatot, háromszögeket amik az árnyékot vető felületként szolgálnak, és a fényforrások helyét és erejét.

A program kimenete egy Blender3D-ben használható script kód amit futtatva láthatjuk a bevitt adatainkat egy 3D-s környezetben illetve a futás végeredményét is. További kimenet egy másik hasonló script kód ami a hibakezeléshez használható és a sugárkövetés irányait mutatja.

A sugárkövetés rekurzívan zajlik a kezdeti állapot hogy egy gömbön véletlen szerű irányba elindul egy sugár, ha ez a sugár háromszögbe ütközik, akkor a rekurzió beljebb lép egy szintel és újabb követés indul az eltalált háromszögre illeszkedő félgömbön. A rekurzió akkor áll le ha a sugár nem talál el semmit, eléri a megadott maximum mélységet, vagy egy fényforrást talál el, ebben az esetben visszatér a fényforrás erejével, egyéb esetben nullával.

A rekurzió mélysége minden futtatásomnál 3 mivel így is nagyon hosszú a program futása.

_Sampling_: a kezdeti és találati pontokból kiinduló sugarak számát jelenti.

Minél nagyobbak ezek a számok annál pontosabb lesz szimuláció, viszont annál több időt is vesz igénybe.

A próba futtatásoknál 25, és 50 samplingel teszteltem a programot, mivel minden ennél kisebb másodpercek alatt végez, és bármi ami ennél nagyobb órákat vesz igénybe.

A program futásának lehetséges végeredménye Blender3D-ben

![alt text](https://github.com/Bence886/perprog_beadando/blob/master/Images/scene.png)

A gömb jelzi a fényforrást (alapból nem gömb csak egy pont de a képen így jobban látszódik) A keresés a 0, 0, 0 és a 0, 0, 1 pontból indult. Két nagy háromszög automatikusan bekerül a térbe a bemenettől függetlenül, ezek jelképezik a talajt, a két kisebb háromszög árnyékolja a fényt.

Ez a végeredmény egy 25 samplingel futtatott keresés eredménye, ami nem volt elég pontos ahhoz, hogy megtalálja a fényforrást a kis résen át. A sugárkövetést, ha képalkotáshoz használják, jó minőség elérése érdekében ezer fölötti sampling szükséges általában. (a rés a két háromszög között nem pont a keresési pont és a fényforrás között van, hanem picit arrébb ez azért fontos, mert ha egyből van rálátás akkor a program megtalálja a rekurzió első lépésében a fényt és nem is keres tovább)

A hibakereséshez használt script fájl futtatása pedig a következő eredményt adja.

A félgömbök a különböző eltalált felületekről a további keresések irányát mutatják. Kijelöltem párat a jobb átláthatóság kedvéért.

 ![](https://github.com/Bence886/perprog_beadando/blob/master/Images/sampling_scene.png)

**UML osztálydiagram** :

 ![](https://github.com/Bence886/perprog_beadando/blob/master/Images/uml.png)

**Szekvenciális megoldás:**

Az diagramok x tengelye az időt jelöli másodpercben. Az y tengely pedig a processzor kihasználtságot %-ban.

Szekvenciális futtatás 25-ös samplingel. ![](https://github.com/Bence886/perprog_beadando/blob/master/Images/sequential_sequential.png)

**Párhuzamosítás:**

A sugárkövetés egy jól párhuzamosítható probléma, általában GPU-t használnak hozzá és minden egyes sugár külön szálon számolódik. Viszont mivel most C# nyelvről és CPU-n történő szinkronizálásról van szó ezért ez nem lenne a leghatékonyabb megoldás lévén a CPU-n csak 4-16 mag van míg egy GPU-ban akár több ezer mag is található.

Ekkora mértékű párhuzamosítás viszont túlságosan fragmentálná a problémát, túl sok overheadet jelentene a szálak létrehozása és kezelése.

Ezért elsőre azt párhuzamosítottam hogyha több pont is van amire fut a szimuláció akkor azok egyszerre fussanak.

 ![](https://github.com/Bence886/perprog_beadando/blob/master/Images/paralel_sequential.png)

Jól látható hogy a futási idő is kevesebb lett és a processzor kihasználtság is duplázódott. A visszaesést 45 másodperc körül az okozza hogy az egyik keresési pont szimulálása véget ért.

Ezután megpróbáltam hogy javulna-e a teljesítmény ha a külön szimulálás helyett ez a rész szekvenciális maradna, viszont a sugárkövetés 0. szintjén indítanék minden hívásra egy új szálat. Ez 25-ös samplingnél 25 szálat jelent, ezután a rekurzió többi szintje már nem indít túl szálat fentebb említet okok miatt.

 ![](https://github.com/Bence886/perprog_beadando/blob/master/Images/sequential_paralel.png)

Az ábrán látható hogy a futásidő a negyedére csökkent a teljesen szekvenciálishoz képest és közel maximális a processzor kihasználtság. Kis megjegyzés hogyha a program profiler és debugger nélkül fut akkor 100%-os a processzor kihasználtság. A visszaesés a szimuláció felénél az első keresési pont végét és a második kezdetét jelenti, itt szűntek meg a szálak és indultak az újak.

Ezután mind a két eddigi ponton bevezettem a párhuzamosságot ami 25 ös sampling esetén 50 szálat jelent plusz a 2 szálat ami indítja a sugárkövetéseket és a végén összegyűjti az eredményt is. ![](https://github.com/Bence886/perprog_beadando/blob/master/Images/paralal_paralel.png)

A futási idő így még kevesebb lett, és a processzor kihasználtság is közel száz százalékos.

A futási idők az előbbiekben említett párhuzamosításokkal 25 és 50 samplingel egymáshoz képest futási időben a következőképp alakulnak:




|   |
| --- |

 ![](https://github.com/Bence886/perprog_beadando/blob/master/Images/diagrams.png)

Ebből látszik hogy kis és nagy adatmennyiség esetén is a sugárkövetés párhuzamosítása jelenti a nagyobb teljesítménybeli javulást, ami várható is volt mivel az a leg számítás igényesebb rész. A külön keresések párhuzamosítása önmagában és a másik párhuzamosítással együtt is javít a futási időn de nem túl jelentősen.

