# Digitális Könyvtárkezelő Alkalmazás

## Feladatleírás

Hozz létre egy **Digitális Könyvtárkezelő Alkalmazást**, amely lehetővé teszi:

-   **Könyvek felvételét** a könyvtár adatbázisába (pl. könyvcím, szerző, kiadási év, kategória).

-   **Könyvek listázását**, amelyek különböző kategóriák szerint szűrhetők (pl. regény, tudományos, ismeretterjesztő stb.).
-   **Könyvek szerkesztését vagy törlését**, ha hibás adatokat adtunk meg.
-   **Felhasználók kezelését**, ahol az adminisztrátor felhasználó külön jogokkal rendelkezik:
    -   Könyvek adatainak szerkesztése.
    -   Felhasználók hozzáadása és törlése.
-   Az alkalmazás **fájlba menti és visszaolvassa az adatokat** (könyveket és felhasználókat), így újraindítás után is elérhetők.

## **Feladat Funkciói**

### Főképernyő

A főképernyő tartalmazza:

-   **Bejelentkezés** : Felhasználóként vagy adminisztrátorként lehet belépni.
-   **Regisztráció** : Új felhasználó létrehozása.
-   **Belépés után** : A menüben az alábbi funkciók érhetők el:
    -   Könyvek listázása.
    -   Könyvek hozzáadása.
    -   Könyvek szerkesztése vagy törlése (csak adminisztrátornak).

### Regisztráció

Új felhasználó regisztrálásához az alábbi adatokat kell megadni:

-   **Felhasználónév** (egyedi kell legyen).
-   **Jelszó** (legalább 8 karakter, tartalmazzon nagybetűt, kisbetűt és számot).
-   **Email** (valós e-mail formátum ellenőrzése).
-   Az első regisztrált felhasználó automatikusan **adminisztrátor** lesz.

**Extra szabályok:**

-   Egy felhasználónév nem lehet már létező.
-   A jelszó validációját a megadott szabályok szerint kell elvégezni.

### Könyvek kezelése

A belépés után a felhasználók hozzáférhetnek a következő funkciókhoz:

-   **Könyv felvétele:**

    -   Könyvcím (kötelező mező).
    -   Szerző neve (kötelező mező).
    -   Kiadási év (csak szám lehet, 1800 és az aktuális év között).
    -   Kategória (választható, például regény, tudományos, ismeretterjesztő, egyéb).

-   **Könyv listázása:**

    -   Az összes könyv megjelenik egy listában.
    -   Szűrés kategória szerint (pl. csak regények megjelenítése).
    -   Az adatok megjelenítéséhez használj ListBox vagy DataGrid elemet.

-   **Könyv szerkesztése:**

    -   A felhasználó kiválaszthat egy könyvet a listából, és módosíthatja az adatait.
    -   Csak adminisztrátor végezhet szerkesztést.

-   **Könyv törlése:**

    -   A felhasználó kiválaszthat egy könyvet a listából, és törölheti azt.
    -   Csak adminisztrátor végezhet törlést.

### Fájlkezelés

Az alkalmazás fájlba menti az adatokat, hogy újraindítás után is elérhetők legyenek:

-   **Felhasználók mentése:**

    -   Felhasználók adatai (email, felhasználónév, jelszó, jogosultság) mentésre kerülnek egy users.txtfájlba.

-   **Könyvek mentése:**

    -   Könyvek adatai (cím, szerző, kiadási év, kategória) mentésre kerülnek egy books.txt fájlba.

### Adminisztrátor Jogosultságok

Az adminisztrátor a következő különleges jogokkal rendelkezik:

-   **Felhasználók listázása:**

    -   Az adminisztrátor megtekintheti az összes felhasználót egy külön listában.

-   **Felhasználók törlése:**

    -   Az adminisztrátor törölhet más felhasználókat (de önmagát nem törölheti).

-   **Könyvek kezelése:**

    -   Az adminisztrátor szerkesztheti és törölheti az összes könyvet.
