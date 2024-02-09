import pygame
pygame.init()
ekran = pygame.display.set_mode((800,600))
class Kulka:
	def __init__(self, x, y, wielkosc, kolor, predkosc, ekran):
		self.x = x
		self.y = y
		self.wielkosc = wielkosc
		self.kolor = kolor
		self.predkosc = predkosc
		self.ekran = ekran
		self.kierunekX = 1
		self.kierunekY = 1

	def rysuj(self):
		pygame.draw.circle(ekran, self.kolor, (self.x, self.y), self.predkosc)

	def ruch(self):
		self.x += self.predkosc*self.kierunekX
		self.y += self.predkosc*self.kierunekY

		if self.y <= 0 or self.y >= 600:
			self.kierunekY *= -1

		if self.x <= 0:
			return 1

		if self.x >= 800:
			return 2

	def update(self):
		ruch()
		rysuj()

	def reset(self):
		self.x = 400
		self.y = 300
		self.kierunekX *= -1

	def odbicie(self):
		self.kierunekX *= -1