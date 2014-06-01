#include "character.h"

Character::Character(int x = 0, int y = 0) 
{
	//struct State
	state.animation = FALL;
	state.enviro = AIR;
	state.turn = LEFT;
	state.djump = false;

	//struct Velocity
	velocity.x = 0;
	velocity.y = 0;
	velocity.acc = ACCELERATION;
	velocity.gravity = GRAVITY;

	//struct weapon
	//weapon 0 - always enabled
	weapons[0].enabled = true;
	weapons[0].ammo = 0;
	for(int i = 1; i < WEAPON_QNTY;i++)
	{
		weapons[i].enabled = false;
		weapons[i].ammo = 0;
	}

	//struct sprite
	sprite.x = x;
	sprite.y = y;

	sprite.w = CHAR_WIDTH;
	sprite.h = CHAR_HEIGHT;
	sprite.offsetX = 0.1 * CHAR_WIDTH;
	sprite.offsetY = 0.1 * CHAR_HEIGHT;
	sprite.frame = 0;
	sprite.surface = NULL;

	//other
	health = 100;
	
}

Character::~Character()
{
	SDL_FreeSurface(sprite.surface);
}

void Character::RandomizePosition()
{
	do{
		sprite.x = GetRandomNumber(SCREEN_WIDTH);
		sprite.y = GetRandomNumber(SCREEN_HEIGHT);

	} while(checkCollision(mapa.GetElements()));
}

void Character::handle_input()
{
	Uint8 *keystates = SDL_GetKeyState(NULL);
	if(event.type == SDL_KEYDOWN )
	{
		switch(event.key.keysym.sym)
		{
		case SDLK_UP:
			switch(state.enviro)
			{
			case AIR:
				if(!state.djump)
				{
					state.djump = true;
					velocity.y = -VELOCITY_Y;
				}
				break;
			case WALL:
				velocity.x *= -WALL_JUMP;
			case GROUND:
				state.enviro = AIR;
				velocity.y = -VELOCITY_Y;
			}
			break;
		case SDLK_LEFT: 
			if(state.enviro != WALL)
			{
				if(!keystates[SDLK_RIGHT])
				{
					velocity.x = -VELOCITY_X;				
					state.animation = WALK;
					state.turn = LEFT;
				}
			}
			break;
		case SDLK_RIGHT: 		
			if(state.enviro != WALL)
			{
				if(!keystates[SDLK_LEFT])
				{
					velocity.x = VELOCITY_X;
					state.animation = WALK;
					state.turn = RIGHT;
				}
			}
			break;

		case SDLK_F1:
			hud = !hud;
			break;
		case SDLK_F2:
			mapa.GenerateMap(GetRandomColor(screen));
			bron.Clear();
			RandomizePosition();
			break;
		case SDLK_RIGHTBRACKET:
			velocity.gravity+=0.25;
			break;
		case SDLK_LEFTBRACKET:
			velocity.gravity-=0.25;
			break;
		case SDLK_RETURN:
			if(keystates[SDLK_LALT])
				SDL_WM_ToggleFullScreen(screen); // try fullscreen (only x11)
			break;

		case SDLK_ESCAPE:
			quit = true;
			break;

		case SDLK_SPACE:
			if(state.turn == LEFT)
				bron.Shoot(sprite.x-1, sprite.y + CHAR_HEIGHT/2, -1);
			else if(state.turn == RIGHT)
				bron.Shoot(sprite.x+CHAR_WIDTH+1, sprite.y + CHAR_HEIGHT/2, 1);
			break;
		}
	}
	else if(event.type == SDL_KEYUP)
	{
		switch(event.key.keysym.sym)
		{
		case SDLK_UP: 
			break;
		case SDLK_RIGHT: 
			if(velocity.x > 0 && state.enviro != AIR)
			{
				velocity.x = 0;
				state.animation = STOP;
			}
			break;
		case SDLK_LEFT:  
			if(velocity.x < 0 && state.enviro != AIR)
			{
				velocity.x = 0;
				state.animation = STOP;
			}
			break;
		}
	}
}

void Character::Dynamic() 
{
	switch(state.enviro)
	{
	case GROUND:
		if(velocity.x > 0)
		{
			if((velocity.x += velocity.acc) > MAX_VELOCITY_X)
				velocity.x = MAX_VELOCITY_X;
		}
		else if(velocity.x < 0)
		{
			if((velocity.x -= velocity.acc) < -MAX_VELOCITY_X)
				velocity.x = -MAX_VELOCITY_X;
		}
		break;
	case AIR:
		if((velocity.y += velocity.gravity) > MAX_VELOCITY_Y)
			velocity.y = MAX_VELOCITY_Y;
		break;
	}
}

void Character::Move(float timeFactor, std::vector<SDL_Rect> collElements)
{
	Uint8 *keystates = SDL_GetKeyState(NULL);
	//move y axis
	float move;
	if(state.enviro == GROUND)
	{
		move = 1;
	}
	else
	{
		move = velocity.y * timeFactor; // move
	}
	sprite.y += move; // move
	

	if(checkCollision(collElements) || checkCollision(bron.GetElements())) // check for collision
	{
		switch(state.enviro)
		{
		case GROUND:
			break;
		case AIR:
			if(velocity.y < 0) // awww, i hit ceiling
			{
				velocity.y = 0;
			}
			else // hit ground
			{
				velocity.y = 0;
				state.enviro = GROUND;
				state.djump = false;
				if((!keystates[SDLK_LEFT] && velocity.x < 0) || (!keystates[SDLK_RIGHT] && velocity.x > 0))
					velocity.x = 0;
			}
			break;
		case WALL: // i hit a ground
			velocity.y = 0;
			state.enviro = GROUND;
			break;
		}
		sprite.y -= move; 
	}
	else if(state.enviro == GROUND)
	{
		state.enviro = AIR;
	}
	if(sprite.y > SCREEN_HEIGHT+SCREEN_HEIGHT/18)
		sprite.y = -SCREEN_HEIGHT/20;

	if(state.enviro == WALL)
	{
		move = velocity.x;
	}
	else
	{
		move = velocity.x * timeFactor;
	}
	// move x axis
	sprite.x += move;
	if(checkCollision(collElements) || checkCollision(bron.GetElements()))
	{
		switch(state.enviro)
		{
		case GROUND:
			velocity.x = 0;
			break;
		case AIR:
			state.enviro = WALL;
			state.djump = false;
			velocity.y = STATICAL_GRAVITY;
			if(velocity.x > 0)
				velocity.x = 2;
			else 
				velocity.x = -2;
			if(!keystates[SDLK_LEFT] && !keystates[SDLK_RIGHT])
				velocity.x = 0;

			break;
		case WALL:
			break;
		}
		sprite.x -= move;
	}
	else if(state.enviro == WALL)
	{
		state.enviro = AIR;
	}

	if(sprite.x > SCREEN_WIDTH)
	{
		sprite.x = 0;
		sprite.y--;
	}
	else if(sprite.x < 0)
	{
		sprite.x = SCREEN_WIDTH-10;
		sprite.y--;
	}


}

void Character::Show(SDL_Surface* destination)
{
	static SDL_Rect clip;
	clip.x = 0; clip.y = 0; clip.h=30;
	static int frame_n = 0;
	static int frame_row = 0;
	static int animDelay = 2;

	switch(state.animation)
	{
	case WALK:
		if(state.turn == LEFT)
		{	
			animDelay = 20;
			frame_n = 9;
			clip.w = 25;
			frame_row = 2;
		}
		else if(state.turn == RIGHT)
		{
			animDelay = 20;
			frame_n = 9;
			clip.w = 25;
			frame_row = 1;
		}
		break;
	case IDLE:
		animDelay = 15;
		frame_n = 10;
		clip.w = 20;
		frame_row = 0;
		break;
	case JUMP:
	case FALL:
	case CROUCH:
	case STOP:
		if(state.turn == LEFT)
		{
			frame_n = 0;
			clip.w = 25;
			frame_row = 2;
		}
		else if(state.turn == RIGHT)
		{
			frame_n = 0;
			clip.w = 25;
			frame_row = 1;
		}
		break;
	default:
		break;
	}

	clip.x = (sprite.frame/animDelay)*clip.w;
	clip.y = frame_row * 30;

	apply_surface(sprite.x,sprite.y,sprite.surface, destination, &clip);
	
	sprite.frame++;
	if(sprite.frame >= animDelay*frame_n)
	{
		if(state.animation == IDLE)
		{
			state.animation = STOP;
		}
		sprite.frame = 0;
	}
}

bool Character::checkCollision(std::vector<SDL_Rect> collElements)
{			
	int left,right,bottom, top;
	left = sprite.x + sprite.offsetX;
	right = sprite.x + sprite.w - sprite.offsetX;
	top = sprite.y + sprite.offsetY;
	bottom = sprite.y + sprite.h - sprite.offsetY;

	for(int i = 0; i<collElements.size();i++)
	{
		if(top >= collElements.at(i).y+collElements.at(i).h)
			continue;
		if(bottom <= collElements.at(i).y)
			continue;
		if(left >= collElements.at(i).x+collElements.at(i).w)
			continue;
		if(right <= collElements.at(i).x)
			continue;
		return true;
	}
	return false;
}

bool Character::checkCollision(std::vector<shoot> collElements)
{
	std::vector<SDL_Rect> elements;
	for(int i = 0; i < collElements.size();i++)
		elements.push_back(collElements.at(i).shape);
	return checkCollision(elements);
}

std::vector<SDL_Surface*> Character::PlayerInfo()
{
	SDL_Surface* message1;
	std::vector<SDL_Surface*> vector;
	std::ostringstream temp;

	temp.str(std::string());
	temp << "STATE:    ENVIRO: " << (int)state.enviro;
	message1 = TTF_RenderText_Solid(font, temp.str().c_str(), textColor);
	vector.push_back(message1);
	
	temp.str(std::string());
	temp << "VELOCITY:    x: " << velocity.x << ", y: " << velocity.y << ", acc: " << velocity.acc << ", grav: " << velocity.gravity;
	message1 = TTF_RenderText_Solid(font, temp.str().c_str(), textColor);
	vector.push_back(message1);

	temp.str(std::string());
	temp << "SPRITE:     x: " << sprite.x << ", y: " << sprite.y << ", w: " << sprite.w << ", h: " << sprite.h
	<< ", offsetX: " << sprite.offsetX << ", offsetY: " << sprite.offsetY << ", frame: " << sprite.frame;
	message1 = TTF_RenderText_Solid(font, temp.str().c_str(), textColor);
	vector.push_back(message1);

	return vector;
}

// Banana character!
Banana::Banana(int x, int y) : Character(x, y)
{
	BananaRage = 0;
}

bool Banana::LoadFiles()
{
	sprite.surface = load_image("banan.png");
	if(sprite.surface == NULL)
		return false;
	return true;
	//return load_files("banan.png", sprite);
}
