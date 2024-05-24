// Attendre que le contenu de la page soit complètement chargé
document.addEventListener('DOMContentLoaded', (event) => {
    // Récupérer la zone tactile #touch-area
    const touchArea = document.getElementById('touch-area');

    // Initialiser Socket.io
    const socket = io();
    
    // Variables pour les coordonnées de début et fin du glissement
    let startX, startY, endX, endY;

    // Ajouter un écouteur d'événement 'touchstart' à 'touchArea' pour le début du glissement, se produit lorsqu'un point de contact est placé sur l'élément
    touchArea.addEventListener('touchstart', (e) => {
        // Récupérer le premier point de contact
        // e.touches est une propriété de l'événement TouchEvent qui contient une TouchList de tous les points de contact actifs sur l'écran lors d'un événement de touch. Chaque élément de cette liste est un objet Touch qui contient des informations sur un point de contact individuel.
        // Accède au premier objet Touch dans la TouchList, premier point de contact
        const touchStart = e.touches[0];

        // Stocker les coordonnées de début du glissement
        // Chaque point de contact, c'est-à-dire chaque doigt touchant l'écran, est représenté par un objet Touch. 
        // clientX : La coordonnée X du point de contact par rapport au viewport, pareil pour clientY
        // -> position excate du point de contact sur l'écran
        startX = touchStart.clientX;
        startY = touchStart.clientY;
    });

    // Ajouter un écouteur d'événement 'touchend' à la 'touchArea' pour la fin du glissement, se produit lorsqu'un point de contact se retire de l'écran
    touchArea.addEventListener('touchend', (e) => {
        // Récupérer le point de contact final
        // 'changedTouches' contiend les points de contact qui ont été retirés
        const touchEnd = e.changedTouches[0];
        // Stocker les coordonnées de fin du glissement
        endX = touchEnd.clientX;
        endY = touchEnd.clientY;
        
        // Calculer les différences de coordonnées (vecteur directionnel)
        const deltaX = endX - startX;
        const deltaY = endY - startY;

        // Créer un objet pour représenter le vecteur directionnel
        const directionVector = {
            x: deltaX, // Différence en X
            y: deltaY  // Différence en Y
        };

        // Afficher le vecteur directionnel dans la console
        console.log('Direction Vector:', directionVector);

        // Constante Z
        const Z = 0;

        // Convertir le vecteur directionnel en string au format '-X-Y-Z'
        const vectorString = `#${directionVector.x}#${directionVector.y}#${Z}`;

        // Afficher le vecteur directionnel sous forme de string dans la console
        console.log('Direction Vector String:', vectorString);

        // Envoyer le vecteur directionnel sous forme de string au serveur
        socket.emit('input1', vectorString);
    });
});
