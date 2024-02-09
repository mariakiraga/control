import pygame
from kulka import *

pygame.init()

SZEROKOSC = 800
WYSOKOSC = 600

ekran = pygame.display.set_mode((SZEROKOSC, WYSOKOSC))
pygame.display.set_caption("pong")

zegar = pygame.time.Clock()
FPS = 30

# 1. klasa gracza -> konstruktor, f. rysowania, f. update, f. wyswietlanie punktow
# 2. klasa kulki -> konstruktor, f. rysowania, f. update, f. reset, f. odbicie


# gracz1
# gracz2
kulka = Kulka(SZEROKOSC/2, WYSOKOSC/2, 20, (255,255,255), 10, ekran) # x, y, wielkosc, kolor, predkosc, ekran):


gramy = True
while gramy:
	for event in pygame.event.get():
		if event.type == pygame.QUIT:
			pygame.quit()
			quit()
	ekran.fill((0,0,0))
	kulka.rysuj()

	pygame.display.update()
	zegar.tick(FPS)