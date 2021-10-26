echo "Create a new migration"
PROJECT=CleanArchitecture
MIGRATION=$1

if [ -d $MIGRATION ]; then
  read -p "Name: " MIGRATION
fi;

echo "Creating migration '$MIGRATION'..."
dotnet ef migrations add $MIGRATION -c AppDbContext -s ./$PROJECT.Api -p ./$PROJECT.Infrastructure -o ./Persistence/Migrations

echo "Migration created"