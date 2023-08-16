INCLUDE ../../Global Asset.ink

EXTERNAL PindahScene(sceneName)
Kau mau keluar ??? #speaker: Narator
    + [Iya]
        ~PindahScene("Kota")
        ->DONE
    + [Tidak]
        ->DONE

->END