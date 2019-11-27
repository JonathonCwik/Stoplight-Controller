apt-get -y install nginx

if [ -f /etc/nginx/sites-available/default ]; then
            echo "Nginx already installed"
else 
	sudo apt-get -y install nginx
fi
sudo sh -c "echo '
server {
	listen 80;
	location / {
		proxy_pass http://localhost:50505;
		proxy_http_version 1.1;
		proxy_set_header Upgrade \\$http_upgrade;
		proxy_set_header Connection \\$http_connection;
		proxy_set_header Host \\$http_host;
		proxy_cache_bypass \\$http_upgrade;
		proxy_set_header \\X-Forwarded-For $proxy_add_x_forwarded_for;
	}
}' >> /etc/nginx/sites-available/default"
fi
sudo systemctl restart nginx