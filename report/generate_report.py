from reportlab.lib.colors import HexColor
from reportlab.lib.pagesizes import A4
from reportlab.lib.styles import ParagraphStyle, getSampleStyleSheet
from reportlab.lib.units import cm
from reportlab.platypus import Paragraph, SimpleDocTemplate, Spacer

output = "report/rapport_avancement.pdf"
styles = getSampleStyleSheet()
title = ParagraphStyle("title", parent=styles["Title"], textColor=HexColor("#10141D"), fontSize=20, leading=24, spaceAfter=14)
heading = ParagraphStyle("heading", parent=styles["Heading2"], textColor=HexColor("#FF7A00"), fontSize=12, leading=15, spaceBefore=8, spaceAfter=4)
body = ParagraphStyle("body", parent=styles["BodyText"], fontSize=10.5, leading=14, spaceAfter=7)

story = [
    Paragraph("Rapport d'avancement - Atelier DevMobile 4", title),
    Paragraph("Projet : Kouta Calculator (.NET MAUI)", body),
    Paragraph("Avancement", heading),
    Paragraph("L'interface et la logique de la calculatrice sont réalisées. L'application couvre l'addition, la soustraction, la multiplication, la division, les décimales, le changement de signe, le pourcentage, l'effacement du dernier caractère et la remise à zéro. Une division par zéro affiche un message lisible sans interrompre l'application.", body),
    Paragraph("Interface responsive", heading),
    Paragraph("La page emploie Grid pour la structure et le pavé numérique, VerticalStackLayout pour l'afficheur, HorizontalStackLayout pour les commandes utilitaires, ScrollView pour les expressions longues et Border pour isoler l'écran. Cette composition conserve une disposition lisible sur les petits écrans et lors d'un changement d'orientation.", body),
    Paragraph("État de remise", heading),
    Paragraph("Le code source est prêt à être ouvert dans Visual Studio avec la charge de travail .NET MAUI, puis testé sur un émulateur Android ou Windows. Restent à réaliser sur le poste équipé : la compilation/exécution, l'enregistrement de la vidéo de démonstration (15 secondes maximum, écran et webcam) et le dépôt du projet sur GitHub public.", body),
]
SimpleDocTemplate(output, pagesize=A4, rightMargin=2*cm, leftMargin=2*cm, topMargin=2*cm, bottomMargin=2*cm).build(story)
